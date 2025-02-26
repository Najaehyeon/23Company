using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_gyutae : MonoBehaviour
{
    Animator animator; // 유니티에서 가져오기
    Rigidbody2D _rigidbody;

    public CapsuleCollider2D capsuleCollider2D; // 캡슐 콜라이더2d 참조
    private Vector2 normalcolliderSize; // 기본크기 저장
    private Vector2 slideColliderSize = new Vector2(3f, 2.8f); // 슬라이드시 사이즈
    private Vector2 normalcolliderOffset; // 기본 오프셋 저장
    private Vector2 slideColliderOffset = new Vector2(0f, -2.2f); // 슬라이드시 콜라이더 위치 이동

    public float playerJumpPower = 10f; // 점프하는 힘
    public float forwardSpeed = 5f; // 전진 속도
    public bool isDead = false; // 생사여부 확인
    float deathCooldown = 0f; // 죽는 모션 딜레이
    public bool slide; // 슬라이드 작동
    private bool ground;
    private int jumpCount = 0;
    bool isRun = false; // 점프유무 체크 요소 // 기본행동



    // Start is called before the first frame update
    void Start()
    { // InChildren 을 붙여 하위 오브젝트들에게도 탐색 적용
        animator = GetComponentInChildren<Animator>(); // 작성중인 스크립트가 부착된 오브젝트에게 내가 찾고있는 
        _rigidbody = GetComponent<Rigidbody2D>(); // 컴포넌트가 있는지 탐색후 반환
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        normalcolliderSize = capsuleCollider2D.size;
        normalcolliderOffset = capsuleCollider2D.offset;

        if (animator == null)
            Debug.Log("ani error");

        if (_rigidbody == null)
            Debug.Log("rigid error");
    }

    // Update is called once per frame
    void Update()
    {
        ground = CheckGround();

        if (ground) // 바닥에 있을때
        {
            jumpCount = 0; // 점프횟수 초기화
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 1 && !slide)
        { // 2단 점프 제한
            Jump();
            jumpCount++;
            ground = false;
        }

        if (ground && Input.GetKey(KeyCode.LeftShift)) // 바닥에 있을때만 슬라이드 가능
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

        isDead = true;
        deathCooldown = 1f;
        animator.SetInteger("IsDie", 1); // 애니메이터에 "IsDie"라는 파라미터의 값을 1로 설정(블럭연결)
        
    }

    void Jump()
    {
        if(slide)
        {
            Slide(false);
            animator.SetBool("IsJump", true);
            slide = false;
        }

        if (jumpCount == 0)
        {
           // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1단 점프
            _rigidbody.AddForce(Vector3.up*playerJumpPower, ForceMode2D.Impulse);
        }
    }
    void Slide(bool isSlide)
    {
        if (slide == isSlide) return; // 현재 상태와 동일하면 무시

        slide = isSlide;// 슬라이드 실행
        animator.SetBool("IsSlide", isSlide);

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
        // Physics2D.Raycast 물리충돌을 감지하는 레이저 발사 , 플레이어 위치에서 레이저 발사
        // Vector2.down 아래 방향으로 2.5f 만큼의 길이를 발사. 바닥이 있는지 검사
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        
        if (hit.collider != null) // 바닥에 충돌한 오브젝트가 있다면
        {
            animator.SetBool("IsJump", false);
            return true; // 바닥이 감지되면 점프 초기화
        }
        else
        {
            animator.SetBool("IsJump", true); // 공중에 있다면 
            return false;
        }
    }
}
