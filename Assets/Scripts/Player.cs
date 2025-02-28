using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Animator animator; // ����Ƽ���� ��������
    Rigidbody2D _rigidbody;
    AnimationHandler animationHandler;
    SpriteRenderer spriteRenderer;
    Coroutine coroutine;
    DoctorComtroller doctorComtroller;
    public CapsuleCollider2D capsuleCollider2D; // ĸ�� �ݶ��̴�2d ����

    private Vector2 normalcolliderSize; // �⺻ũ�� ����
    private Vector2 normalcolliderOffset; // �⺻ ������ ����
    private Vector2 slideColliderSize = new Vector2(3f, 2.8f); // �����̵�� ������
    private Vector2 slideColliderOffset = new Vector2(0f, -2.2f); // �����̵�� �ݶ��̴� ��ġ �̵�
    private Vector3 DoctorSponPosition = Vector3.zero;

    public float playerJumpPower = 15f; // �����ϴ� ��
    private float forwardSpeed = 12f; // ���� �ӵ�
    private float forwardSpeed_before = 12f; // ���� �ӵ�
    public float currentHealth = 0f;
    public float maxHealth = 100f;
    public int ObstacleDamage = -10;
    public bool isInvincible = false;
    public float invincibleDuration = 1f;
    private float lastScorePosition = 0f;  // ���������� ������ ���� ��ġ
    private float HitTime = 1f;

    public bool isDead = false; // ���翩�� Ȯ��
    float deathCooldown = 1f; // �״� ��� ������

    public bool slide = false; // �����̵� �۵�
    private bool ground;
    private int jumpCount = 0;
    float lastPosition_Y = 0f;
    bool isRun = false; // �������� üũ ��� // �⺻�ൿ
    float Position = 0f;
    bool sponbool = false;
    bool diebool = false;
    public GameObject prefabToSpawn; // Inspector���� �Ҵ��� ������

    ItemManager itemManager;

    private static Player _instance = null;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SingletonExample �ν��Ͻ��� �����ϴ�!");

            return _instance;

        }
    }
    GameUI gameUI;

    private void Awake()
    {
        init();
    }

    public void init()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        //�÷��̾� ��ġ �ʱ�ȭ 
        gameObject.transform.position = new Vector3(0f, -4.8f);

        //ü�� �ʱ�ȭ 
        currentHealth = maxHealth;
        currentHealth = 100;
        //�ӵ��ʱ�ȭ
        forwardSpeed_before = 12f; // ���� �ӵ�

    }

    // Start is called before the first frame update
    void Start()
    { // InChildren �� �ٿ� ���� ������Ʈ�鿡�Ե� Ž�� ����
        animator = GetComponentInChildren<Animator>(); // �ۼ����� ��ũ��Ʈ�� ������ ������Ʈ���� ���� ã���ִ� 
        _rigidbody = GetComponent<Rigidbody2D>(); // ������Ʈ�� �ִ��� Ž���� ��ȯ
        itemManager = ItemManager.Instance;
        gameUI = FindAnyObjectByType<GameUI>();
        animationHandler = FindObjectOfType<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        doctorComtroller = FindObjectOfType<DoctorComtroller>();
        normalcolliderSize = capsuleCollider2D.size;
        normalcolliderOffset = capsuleCollider2D.offset;

        if (animator == null)
            Debug.Log("ani error");

        if (_rigidbody == null)
            Debug.Log("rigid error");

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �����ϴ�!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (diebool) return;
        Vector3 velocity = _rigidbody.velocity; // _rigidbody �� ���ӵ� ��������
        velocity.x = forwardSpeed;
        _rigidbody.velocity = velocity;
        JumpCheck();

        if (currentHealth <= 0 )
        {
            diebool= true;
           // Time.timeScale = 0f;
            //Die �ִϸ��̼� 
            //�ְ����� ��� 
            float bestscore = PlayerPrefs.GetInt("highscore");
            int currentscore = itemManager.totalScore;
            if (bestscore < currentscore)
            {

                PlayerPrefs.SetInt("highscore", itemManager.totalScore);//������ ����
            }
            //�ױ����� �ִϸ��̼� �����ְ� �״� ��� 
            isDead = true;
            animator.SetInteger("IsDie", 1); // �ִϸ����Ϳ� "IsDie"��� �Ķ������ ���� 1�� ����(������)
            _rigidbody.velocity = Vector2.zero;
            if (!sponbool)
            {
                //���⼭ Doctor�� ����� (����? �Ǵ� �����ΰ� ��ġ�� ����?)
                sponbool = true;
                doctorComtroller.spowinit();

            }
            //gameUI.ActiveGameOverUI();

        }
    }

    private void FixedUpdate() // ���� ������ �ʿ��� �� ������ �������� ȣ��
    {

        if (isDead) return; // isDead �� true ��� �۾����� �ʰ� ��ȯ

        AddScore_position();
    }

    private void OnCollisionEnter2D(Collision2D collision) // �浹�� ���� �̺�Ʈ �߻��� ����
    {
        if (isDead) return; // �̹� �׾����� �浹 ����


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            animator.SetBool("IsJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            animator.SetBool("IsJump", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                // �ִϸ��̼� ����
                Debug.Log("ü�°���");
                AudioManager.instance.SFXPlay(SFXType.Hit);

                Heal(ObstacleDamage); //ü�°���

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(InvincibleRoutine()); // ����
            }
        }

        else if (collision.gameObject.CompareTag("ScoreItem"))
        {
            Debug.Log("��������");
        }
        else if (collision.gameObject.CompareTag("EffectItem"))
        {
        }
    }



    public IEnumerator InvincibleRoutine()
    {
        //���� ���� 
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // ���� ����
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
        spriteRenderer.color = Color.white;
    }

    public void Heal(int amount)    // �÷��̾� HP ȸ��
    {
        currentHealth += amount;  // ü�� ȸ��
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth; // �ִ� ü�� �ʰ� ����
        }
        Debug.Log($"{amount}");
    }


    public void SpeedUp(float amount, float duration)   // �÷��̾� �ӵ� ����
    {

        forwardSpeed += amount; // �ӵ� ����
        Invoke(nameof(ResetSpeed), duration);   // ���� �ð� �� ���� �ӵ��� ����
        Debug.Log("���ǵ��");
    }
    private void ResetSpeed()   // �ӵ��� ������� �ʱ�ȭ
    {
        forwardSpeed = forwardSpeed_before; // ���� �ӵ��� ����(���´� ���߿� 2f ��� �÷��̾� �⺻ �ӵ��� �־��ּ���)
    }


    public void TakeDamage(int amount)
    {
        animationHandler.Hit();
        currentHealth -= amount;  // ü�� ����
        if (currentHealth <= 0)
        {
            currentHealth = 0; // ü���� 0 ���Ϸ� �������� �ʵ��� ����
        }
        Debug.Log("������");
    }


    public IEnumerator TakePoisonDamage(int damage, int times, float interval)  // ���� �ð����� HP �����ϴ� �ڷ�ƾ
    {
        for (int i = 0; i < times; i++)
        {
            TakeDamage(damage);

            //���󺯰�
            spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // ���� ����

            Debug.Log($"�� ������! ���� {damage}, ���� ü�� {currentHealth}, ���� Ƚ�� {times - i - 1}��");
            yield return new WaitForSeconds(interval);  // n��(interval) ���
            spriteRenderer.color = Color.white;
        }
    }





    void Jump()
    {

        if (slide)
        {
            Slide(false);
            animator.SetBool("IsJump", true);
            slide = false;
        }
        //if (jumpCount == 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1�� ����
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }


    }
    private void Slide(bool isSlide)
    {
        if (slide == isSlide) return; // ���� ���¿� �����ϸ� ����

        slide = isSlide;
        animator.SetBool("IsSlide", isSlide); // �ִϸ��̼� ����

        if (isSlide)
        {
            capsuleCollider2D.size = slideColliderSize; // �����̵� ũ��� ����
            capsuleCollider2D.offset = slideColliderOffset;
        }
        else
        {
            animator.SetBool("IsSlide", false);
            capsuleCollider2D.size = normalcolliderSize; // �⺻ ũ��� ����
            Debug.Log("�����̵� ����, �ݶ��̴� ���� ũ��� ������: " + normalcolliderSize);
            capsuleCollider2D.offset = normalcolliderOffset;

        }
    }



    bool CheckGround()
    {
        float rayLength = 2.5f; // ������ ���� ����
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // �ٴ� �׶��� ���̾�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        //lastPosition_Y = transform.position.y;

        //if (hit.collider != null && _rigidbody.velocity.y <0) //���� ��� �ְ� +����������
        //if (hit.collider != null && transform.position.y < lastPosition_Y) //���� ��� �ְ� +����������
        if (hit.collider != null) // �ٴڿ� �浹�� ������Ʈ�� �ִٸ�
        {
            animator.SetBool("IsJump", false);
            // Physics2D.Raycast �����浹�� �����ϴ� ������ �߻� , �÷��̾� ��ġ���� ������ �߻�
            // Vector2.down �Ʒ� �������� 0.3f ��ŭ�� ���̸� �߻�. �ٴ��� �ִ��� �˻�
            return true; // �ٴ��� �����Ǹ� ���� �ʱ�ȭ

        }
        else // ����� ���� �ȴ���ְ� �ö󰡴���?
        {
            animator.SetBool("IsJump", true);
            return false;
        }
    }

    private void AddScore_position()
    {
        float currentPosition = gameObject.transform.position.x;

        // ���� ��ġ�� �����κ�
        int currentBlock = Mathf.FloorToInt(currentPosition);
        // ������ ���� ��ġ�� �����κ�
        int lastBlock = Mathf.FloorToInt(lastScorePosition);

        // ���ο� ����� ���� ������ ���� �߰�
        if (currentBlock > lastBlock)
        {
            itemManager.AddScore(currentBlock - lastBlock);
            lastScorePosition = currentPosition;
        }
    }

    private void JumpCheck()
    {
        //ground = CheckGround();

        //if (ground) // �ٴڿ� ������
        //{
        //    jumpCount = 0; // ����Ƚ�� �ʱ�ȭ
        //}

        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
        { // 2�� ���� ����
            Jump();
            AudioManager.instance.SFXPlay(SFXType.Jump);


            // ground = false;
        }

        if (jumpCount == 0 && Input.GetKey(KeyCode.LeftShift)) // �ٴڿ� �������� �����̵� ����
        {
            Slide(true);
            AudioManager.instance.SFXPlay(SFXType.Slide);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Slide(false);
        }


        if (isDead)
        {
            if (deathCooldown <= 0f) // ������� �����
            {

            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else // ���� ���� ���� 
        {

            if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 1)
            {
                isRun = true;
            }
        }
    }

    public void Flip_X()
    {
        spriteRenderer.flipX = true;
    }

    public void Exit_run(Vector3 direction, float speed)
    {
        //������ �ӵ� �� �������� �̵��ϱ� 
        animator.SetInteger("IsDie", 0);
        transform.position += direction * speed * Time.deltaTime;
    }

    // �� GameObject�� �����ϴ� �޼���
    private void SpawnNewGameObject()
    {
        // �������� �Ҵ�Ǿ����� Ȯ��
        if (prefabToSpawn != null)
        {
            // ���� ��ġ���� �ణ �տ� ����
            Vector3 spawnPosition = transform.position + new Vector3(16f, 0f, 0f);

            // GameObject ����
            GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // �ʿ��ϴٸ� ������ GameObject�� �߰� ����
            // ��: �̸� ����
            newObject.name = "SpawnedObject_" + Time.time;

            Debug.Log("�� GameObject�� �����Ǿ����ϴ�: " + newObject.name);
        }
        else
        {
            Debug.LogError("������ �������� �Ҵ���� �ʾҽ��ϴ�. Inspector���� prefabToSpawn�� �������ּ���.");
        }
    }
    public void SetRunAni(float speed)
    {
        animator.SetInteger("IsDie", 0);
        Vector3 velocity = _rigidbody.velocity; // _rigidbody �� ���ӵ� ��������
        velocity.x = speed;
        _rigidbody.velocity = velocity;
    }
}
