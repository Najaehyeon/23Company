using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    Animator animator; // 유니티에서 가져오기
    Rigidbody2D _rigidbody;
    AnimationHandler animationHandler;
    SpriteRenderer spriteRenderer;
    Coroutine coroutine;

    public float playerJumpPower = 15f; // 점프하는 힘
    public float forwardSpeed = 0f; // 전진 속도
    public float forwardSpeed_before = 0f; // 전진 속도
    public float currentHealth = 0f;
    public float maxHealth = 100f;
    public int ObstacleDamage = -10;
    public bool isInvincible = false;
    public float invincibleDuration = 1f;
    private float lastScorePosition = 0f;  // 마지막으로 점수를 얻은 위치
    private float HitTime = 1f;

    public bool isDead = false; // 생사여부 확인
    float deathCooldown = 0f; // 죽는 모션 딜레이
    public bool slide; // 슬라이드 작동
    private bool ground;
    private int jumpCount = 0;
    float lastPosition_Y = 0f;
    bool isRun = false; // 점프유무 체크 요소 // 기본행동
    float Position = 0f;

    ItemManager itemManager;

    private static Player _instance = null;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SingletonExample 인스턴스가 없습니다!");

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
        //플레이어 위치 초기화 
        gameObject.transform.position = new Vector3(0f, -4.8f);

        //체력 초기화 
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    { // InChildren 을 붙여 하위 오브젝트들에게도 탐색 적용
        animator = GetComponentInChildren<Animator>(); // 작성중인 스크립트가 부착된 오브젝트에게 내가 찾고있는 
        _rigidbody = GetComponent<Rigidbody2D>(); // 컴포넌트가 있는지 탐색후 반환
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
            Debug.LogError("SpriteRenderer가 없습니다!");
        }

        //spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // 색상 변경
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            //Die 애니메이션 
            //최고점수 기록 
            float bestscore = PlayerPrefs.GetInt("highscore");
            int currentscore = itemManager.totalScore;
            if (bestscore < currentscore)
            {
                PlayerPrefs.SetInt("highscore", itemManager.totalScore);//데이터 저장
            }

            gameUI.ActiveGameOverUI();
            //currentHealth = maxHealth;
        }
    }

    private void FixedUpdate() // 물리 연산이 필요할 때 일정한 간격으로 호출
    {

        if (isDead) return; // isDead 가 true 라면 작업하지 않고 반환

        Vector3 velocity = _rigidbody.velocity; // _rigidbody 의 가속도 가져오기
        velocity.x = forwardSpeed;

        if (isRun) // isRun 이 true 일때
        {
            velocity.y += playerJumpPower; // velocity.y 에 점프할때 가속 적용
            //velocity.y = playerJump; // velocity.y 에 점프하는 힘 넣기 차이가 뭐지
            isRun = false;
        }

        _rigidbody.velocity = velocity; // 다시 넣어줘야함

        AddScore_position();
    }

    private void OnCollisionEnter2D(Collision2D collision) // 충돌에 대한 이벤트 발생시 실행
    {
        if (isDead) return; // 이미 죽었으면 충돌 무시

        if (collision.gameObject.CompareTag("Ground"))  // 땅과 충돌하면 죽지 않도록 예외 처리
        {
            ground = true;   // 바닥 감지
            jumpCount = 0;   // 점프 횟수 초기화
            return;          // 죽지 않도록 예외 처리
        }
        //isDead = true;
        //deathCooldown = 1f;
        //animator.SetInteger("IsDie", 1); // 애니메이터에 "IsDie"라는 파라미터의 값을 1로 설정(블럭연결)

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                // 애니메이션 실행
                Debug.Log("체력감소");
                //if (collision.gameObject.name.Contains("Down"))
                //{
                //    animationHandler.Hit();
                //}
                //else
                //{
                //    animationHandler.JumpHit();
                //}
                Heal(ObstacleDamage); //체력감소
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(InvincibleRoutine()); // 무적
            }
        }

        else if (collision.gameObject.CompareTag("ScoreItem"))
        {
            Debug.Log("점수증가");
        }
        else if (collision.gameObject.CompareTag("EffectItem"))
        {
        }
    }



    public IEnumerator InvincibleRoutine()
    {
        //무적 시작 
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.4f); // 색상 변경
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
        spriteRenderer.color = Color.white;
    }

    public void Heal(int amount)    // 플레이어 HP 회복
    {
        currentHealth += amount;  // 체력 회복
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth; // 최대 체력 초과 방지
        }
        Debug.Log($"{amount}");
    }


    public void SpeedUp(float amount, float duration)   // 플레이어 속도 증가
    {
        forwardSpeed_before = forwardSpeed;
        forwardSpeed += amount; // 속도 증가
        Invoke(nameof(ResetSpeed), duration);   // 일정 시간 후 원래 속도로 복귀
        Debug.Log("스피드업");
    }
    private void ResetSpeed()   // 속도를 원래대로 초기화
    {
        forwardSpeed = forwardSpeed_before; // 원래 속도로 복귀(규태님 나중에 2f 대신 플레이어 기본 속도로 넣어주세여)
    }


    public void TakeDamage(int amount)
    {
        animationHandler.Hit();
        currentHealth -= amount;  // 체력 감소
        if (currentHealth <= 0)
        {
            currentHealth = 0; // 체력이 0 이하로 내려가지 않도록 방지
        }
        Debug.Log("데미지");
    }


    public IEnumerator TakePoisonDamage(int damage, int times, float interval)  // 일정 시간마다 HP 감소하는 코루틴
    {
        for (int i = 0; i < times; i++)
        {
            TakeDamage(damage);
            Debug.Log($"독 데미지! 피해 {damage}, 현재 체력 {currentHealth}, 남은 횟수 {times - i - 1}번");
            yield return new WaitForSeconds(interval);  // n초(interval) 대기
        }
    }





    void Jump()
    {
        animator.SetBool("IsJump", true);
        if (jumpCount == 0)
        {
            // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1단 점프
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse);
        }

        else if (jumpCount == 1)
        {
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse); // 2단 점프 힘 증가
        }
    }
    private void Slide(bool isSlide)
    {
        slide = isSlide;
        animator.SetBool("IsSlide", isSlide); // 애니메이션 설정
    }



    bool CheckGround()
    {
        float rayLength = 2.5f; // 레이저 길이 증가
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // 바닥 그라운드 레이어
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        lastPosition_Y = transform.position.y;
        if (hit.collider != null && transform.position.y <= lastPosition_Y) //땅에 닿고 있고 +내려가는중
        {
            animator.SetBool("IsJump", false);
            // Physics2D.Raycast 물리충돌을 감지하는 레이저 발사 , 플레이어 위치에서 레이저 발사
            // Vector2.down 아래 방향으로 0.3f 만큼의 길이를 발사. 바닥이 있는지 검사
            return false; // 바닥이 감지되면 점프 초기화

        }
        else // 여기는 땅에 안닿고있고 올라가는중?
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

        // 현재 위치의 정수부분
        int currentBlock = Mathf.FloorToInt(currentPosition);
        // 마지막 점수 위치의 정수부분
        int lastBlock = Mathf.FloorToInt(lastScorePosition);

        // 새로운 블록을 지날 때마다 점수 추가
        if (currentBlock > lastBlock)
        {
            itemManager.AddScore(currentBlock - lastBlock);
            lastScorePosition = currentPosition;
        }
    }

    private void JumpCheck()
    {
        ground = CheckGround();

        if (ground) // 바닥에 있을때
        {
            jumpCount = 0; // 점프횟수 초기화
        }

        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpCount < 2)
        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
        { // 2단 점프 제한
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
            if (deathCooldown <= 0f) // 죽은경우 재시작
            {

            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else // 죽지 않은 상태 
        {
            //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpCount < 2)
            if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
            {
                isRun = true;
            }
        }
    }

}