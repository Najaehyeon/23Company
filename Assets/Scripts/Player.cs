using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    Animator animator; // ����Ƽ���� ��������
    Rigidbody2D _rigidbody;
    AnimationHandler animationHandler;
    SpriteRenderer spriteRenderer;
    Coroutine coroutine;

    public float playerJumpPower = 15f; // �����ϴ� ��
    public float forwardSpeed = 0f; // ���� �ӵ�
    public float forwardSpeed_before = 0f; // ���� �ӵ�
    public float currentHealth = 0f;
    public float maxHealth = 100f;
    public int ObstacleDamage = -10;
    public bool isInvincible = false;
    public float invincibleDuration = 1f;
    private float lastScorePosition = 0f;  // ���������� ������ ���� ��ġ
    private float HitTime = 1f;

    public bool isDead = false; // ���翩�� Ȯ��
    float deathCooldown = 0f; // �״� ��� ������
    public bool slide; // �����̵� �۵�
    private bool ground;
    private int jumpCount = 0;
    float lastPosition_Y = 0f;
    bool isRun = false; // �������� üũ ��� // �⺻�ൿ
    float Position = 0f;

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

        if (animator == null)
            Debug.Log("ani error");

        if (_rigidbody == null)
            Debug.Log("rigid error");

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �����ϴ�!");
        }

        //spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            //Die �ִϸ��̼� 
            //�ְ����� ��� 
            float bestscore = PlayerPrefs.GetInt("highscore");
            int currentscore = itemManager.totalScore;
            if (bestscore < currentscore)
            {
                PlayerPrefs.SetInt("highscore", itemManager.totalScore);//������ ����
            }

            gameUI.ActiveGameOverUI();
            //currentHealth = maxHealth;
        }
    }

    private void FixedUpdate() // ���� ������ �ʿ��� �� ������ �������� ȣ��
    {

        if (isDead) return; // isDead �� true ��� �۾����� �ʰ� ��ȯ

        Vector3 velocity = _rigidbody.velocity; // _rigidbody �� ���ӵ� ��������
        velocity.x = forwardSpeed;

        if (isRun) // isRun �� true �϶�
        {
            velocity.y += playerJumpPower; // velocity.y �� �����Ҷ� ���� ����
            //velocity.y = playerJump; // velocity.y �� �����ϴ� �� �ֱ� ���̰� ����
            isRun = false;
        }

        _rigidbody.velocity = velocity; // �ٽ� �־������

        AddScore_position();
    }

    private void OnCollisionEnter2D(Collision2D collision) // �浹�� ���� �̺�Ʈ �߻��� ����
    {
        if (isDead) return; // �̹� �׾����� �浹 ����

        if (collision.gameObject.CompareTag("Ground"))  // ���� �浹�ϸ� ���� �ʵ��� ���� ó��
        {
            ground = true;   // �ٴ� ����
            jumpCount = 0;   // ���� Ƚ�� �ʱ�ȭ
            return;          // ���� �ʵ��� ���� ó��
        }
        //isDead = true;
        //deathCooldown = 1f;
        //animator.SetInteger("IsDie", 1); // �ִϸ����Ϳ� "IsDie"��� �Ķ������ ���� 1�� ����(������)

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                // �ִϸ��̼� ����
                Debug.Log("ü�°���");
                //if (collision.gameObject.name.Contains("Down"))
                //{
                //    animationHandler.Hit();
                //}
                //else
                //{
                //    animationHandler.JumpHit();
                //}
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
        forwardSpeed_before = forwardSpeed;
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
            Debug.Log($"�� ������! ���� {damage}, ���� ü�� {currentHealth}, ���� Ƚ�� {times - i - 1}��");
            yield return new WaitForSeconds(interval);  // n��(interval) ���
        }
    }





    void Jump()
    {
        animator.SetBool("IsJump", true);
        if (jumpCount == 0)
        {
            // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1�� ����
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse);
        }

        else if (jumpCount == 1)
        {
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse); // 2�� ���� �� ����
        }
    }
    private void Slide(bool isSlide)
    {
        slide = isSlide;
        animator.SetBool("IsSlide", isSlide); // �ִϸ��̼� ����
    }



    bool CheckGround()
    {
        float rayLength = 2.5f; // ������ ���� ����
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // �ٴ� �׶��� ���̾�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        lastPosition_Y = transform.position.y;
        if (hit.collider != null && transform.position.y <= lastPosition_Y) //���� ��� �ְ� +����������
        {
            animator.SetBool("IsJump", false);
            // Physics2D.Raycast �����浹�� �����ϴ� ������ �߻� , �÷��̾� ��ġ���� ������ �߻�
            // Vector2.down �Ʒ� �������� 0.3f ��ŭ�� ���̸� �߻�. �ٴ��� �ִ��� �˻�
            return false; // �ٴ��� �����Ǹ� ���� �ʱ�ȭ

        }
        else // ����� ���� �ȴ���ְ� �ö󰡴���?
        {
            animator.SetBool("IsJump", true);
            return true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
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
        ground = CheckGround();

        if (ground) // �ٴڿ� ������
        {
            jumpCount = 0; // ����Ƚ�� �ʱ�ȭ
        }

        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpCount < 2)
        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
        { // 2�� ���� ����
            Jump();
            jumpCount++;

            ground = false;
        }

        //if ((Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButtonDown(1)) && ground)
        if ((Input.GetKey(KeyCode.DownArrow)) && ground)
        {
            Slide(true);
        }
        else
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
            //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpCount < 2)
            if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
            {
                isRun = true;
            }
        }
    }

}