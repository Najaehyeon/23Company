using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorComtroller : MonoBehaviour
{
    public GameObject target; // 회전 캐릭터의 Transform
    private float speed = 4.0f; // 이동 속도
    private float runspeed = 6.0f;
    private float stopDistance = 3.0f; // 멈출 거리
    private bool hasReachedTarget = false; // 목표에 도달했는지 여부
    private bool hasPlayedAttackAnimation = false; // 공격 애니메이션 실행 여부
    private bool isAttacking = false; // 공격 중인지 여부
    private bool isMovingAfterAttack = false; // 공격 후 이동 중인지 여부
    private Vector3 moveDirection; // 이동 방향
    private float attackDuration = 2f; // 공격 지속 시간(초)
    private Rigidbody2D rb; // Rigidbody 컴포넌트
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer sr;
    private bool startEnding=false;
    GameUI gameUI;
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>(); // Rigidbody 컴포넌트 가져오기
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
        sr = GetComponentInChildren<SpriteRenderer>();
        gameUI =FindObjectOfType<GameUI>();
        rb.gravityScale = 1f;


    }

    public void spowinit()
    {
        transform.position = target.transform.position + new Vector3(16f, 3f);
        startEnding =true;
        rb.gravityScale= 1f;
        rb.velocity = Vector2.zero;
    }
    private void Update()
    {
        Debug.Log("이동 상태: " + isMovingAfterAttack + ", 위치: " + transform.position);
        if (startEnding)
        {
            if (target != null && !hasReachedTarget)
            {
                // 타겟과의 거리 계산
                //Vector3 moveDir = Vector3.right; // (1,0,0)
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                Vector3 direction = (target.transform.position - transform.position).normalized;
                // 타겟으로부터 stopDistance 이내로 가까워지면 멈춤
                if (distanceToTarget <= stopDistance)
                {
                    hasReachedTarget = true;

                    // 속도를 0으로 설정하여 완전히 멈추게 함
                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                        
                    }

                    // 공격 애니메이션을 아직 실행하지 않았다면 실행
                    if (!hasPlayedAttackAnimation && animator != null)
                    {
                        hasPlayedAttackAnimation = true;
                        StartCoroutine(AttackForDuration(direction));
                    }
                }
                else
                {

                    transform.position += direction * speed * Time.deltaTime;
                    // 필요하다면 이동 애니메이션 설정
                    if (animator != null)
                    {
                        animator.SetBool("IsWalking", true);
                    }

                    Debug.Log("이동 중: " + direction);
                }
            }
            // 공격 후 이동 처리
            else if (isMovingAfterAttack)
            {
                sr.flipX = true;

                // Doctor 캐릭터도 설정된 방향으로 계속 이동
                moveDirection = new Vector3(1f, 0, 0).normalized;
                transform.position += moveDirection * runspeed * Time.deltaTime;

                // 필요하다면 이동 애니메이션 설정
                if (animator != null)
                {
                    animator.SetBool("IsRun", true);
                   
                }

                // Player 이동 시작
                Player.Instance.Exit_run(moveDirection, runspeed);
                StartCoroutine( DieCooldown());
            }
        }

    }
    private IEnumerator DieCooldown()
    {
        //게임종료 
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        gameUI.ActiveGameOverUI();
    }

    private IEnumerator AttackForDuration(Vector3 direction)
    {
        isAttacking = true;
        float attackStartTime = Time.time;

        animator.SetBool("IsWalking", false);
        // 지정된 시간 동안 공격 애니메이션 반복
        while (Time.time - attackStartTime < attackDuration)
        {
            animator.SetTrigger("Attack");

            // 애니메이션 한 번의 길이만큼 대기
            float singleAttackDuration = 1f;
            yield return new WaitForSeconds(singleAttackDuration);
        }

        isAttacking = false;
        Debug.Log("공격 시간 종료");

        isMovingAfterAttack = true;

        transform.position = target.transform.position + new Vector3(-2f, 0f);
        // 이 캐릭터(Doctor)도 같은 방향으로 이동하도록 설정
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // 기존 속도 초기화
        }
    }

    
}
