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
    public CapsuleCollider2D capsuleCollider2D; // 캡슐 콜라이더2d 참조

    private Vector2 normalcolliderSize; // 기본크기 저장
    private Vector2 normalcolliderOffset; // 기본 오프셋 저장
    private Vector2 slideColliderSize = new Vector2(3f, 2.8f); // 슬라이드시 사이즈
    private Vector2 slideColliderOffset = new Vector2(0f, -2.2f); // 슬라이드시 콜라이더 위치 이동
    private Vector3 DoctorSponPosition =Vector3.zero;

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
    float deathCooldown = 1f; // 죽는 모션 딜레이

    public bool slide =false; // 슬라이드 작동
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
        //속도초기화
        forwardSpeed_before = 15f; // 전진 속도

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
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        normalcolliderSize = capsuleCollider2D.size;
        normalcolliderOffset = capsuleCollider2D.offset;

        if (animator == null)
            Debug.Log("ani error");

        if (_rigidbody == null)
            Debug.Log("rigid error");

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 없습니다!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = _rigidbody.velocity; // _rigidbody 의 가속도 가져오기
        velocity.x = forwardSpeed;
        _rigidbody.velocity = velocity;
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
                //죽기전에 애니메이션 보여주고 죽는 모습 
                Vector3 DiePosition=gameObject.transform.position;
                DoctorSponPosition=DiePosition + new Vector3(16f, 0f);
                
                isDead = true;
                animator.SetInteger("IsDie", 1); // 애니메이터에 "IsDie"라는 파라미터의 값을 1로 설정(블럭연결)
                PlayerPrefs.SetInt("highscore", itemManager.totalScore);//데이터 저장
            }

            gameUI.ActiveGameOverUI();
            //currentHealth = maxHealth;
        }
    }

    private void FixedUpdate() // 물리 연산이 필요할 때 일정한 간격으로 호출
    {

        if (isDead) return; // isDead 가 true 라면 작업하지 않고 반환

        AddScore_position();
    }

    private void OnCollisionEnter2D(Collision2D collision) // 충돌에 대한 이벤트 발생시 실행
    {
        if (isDead) return; // 이미 죽었으면 충돌 무시

        
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
                // 애니메이션 실행
                Debug.Log("체력감소");

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
        
        if (slide)
        {
            Slide(false);
            animator.SetBool("IsJump", true);
            slide = false;
        }
        //if (jumpCount == 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1단 점프
            _rigidbody.AddForce(Vector3.up * playerJumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }


    }
    private void Slide(bool isSlide)
    {
        if (slide == isSlide) return; // 현재 상태와 동일하면 무시

        slide = isSlide;
        animator.SetBool("IsSlide", isSlide); // 애니메이션 설정

        if (isSlide)
        {
            capsuleCollider2D.size = slideColliderSize; // 슬라이드 크기로 변경
            capsuleCollider2D.offset = slideColliderOffset;
        }
        else
        {
            animator.SetBool("IsSlide", false);
            capsuleCollider2D.size = normalcolliderSize; // 기본 크기로 복구
            Debug.Log("슬라이드 종료, 콜라이더 원래 크기로 복구됨: " + normalcolliderSize);
            capsuleCollider2D.offset = normalcolliderOffset;

        }
    }



    bool CheckGround()
    {
        float rayLength = 2.5f; // 레이저 길이 증가
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // 바닥 그라운드 레이어
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        //lastPosition_Y = transform.position.y;

        //if (hit.collider != null && _rigidbody.velocity.y <0) //땅에 닿고 있고 +내려가는중
        //if (hit.collider != null && transform.position.y < lastPosition_Y) //땅에 닿고 있고 +내려가는중
        if (hit.collider != null) // 바닥에 충돌한 오브젝트가 있다면
        {
            animator.SetBool("IsJump", false);
            // Physics2D.Raycast 물리충돌을 감지하는 레이저 발사 , 플레이어 위치에서 레이저 발사
            // Vector2.down 아래 방향으로 0.3f 만큼의 길이를 발사. 바닥이 있는지 검사
            return true; // 바닥이 감지되면 점프 초기화

        }
        else // 여기는 땅에 안닿고있고 올라가는중?
        {
            animator.SetBool("IsJump", true);
            return false;
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
        //ground = CheckGround();

        //if (ground) // 바닥에 있을때
        //{
        //    jumpCount = 0; // 점프횟수 초기화
        //}

        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
        { // 2단 점프 제한
            Jump();


           // ground = false;
        }

        if (jumpCount ==0 && Input.GetKey(KeyCode.LeftShift)) // 바닥에 있을때만 슬라이드 가능
        {
            Slide(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
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
            
            if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 1)
            {
                isRun = true;
            }
        }
    }

}