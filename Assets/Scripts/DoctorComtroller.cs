using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorComtroller : MonoBehaviour
{
    public Transform target; // 회전 캐릭터의 Transform
    private float speed = 3.0f; // 이동 속도
    private float runspeed = 5.0f;
    private float stopDistance = 2.0f; // 멈출 거리
    private bool hasReachedTarget = false; // 목표에 도달했는지 여부
    private bool hasPlayedAttackAnimation = false; // 공격 애니메이션 실행 여부
    private bool isAttacking = false; // 공격 중인지 여부
    private bool isMovingAfterAttack = false; // 공격 후 이동 중인지 여부
    private Vector3 moveDirection; // 이동 방향
    private float attackDuration = 2f; // 공격 지속 시간(초)
    private Rigidbody rb; // Rigidbody 컴포넌트
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer sr;
    private void Start()
    {
        transform.position = target.position + new Vector3(16f, 0f);
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트 가져오기
        sr= GetComponentInChildren<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (target != null && !hasReachedTarget)
        {
            // 타겟과의 거리 계산
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            Vector3 direction = (target.position - transform.position).normalized;
            // 타겟으로부터 stopDistance 이내로 가까워지면 멈춤
            if (distanceToTarget <= stopDistance)
            {
                hasReachedTarget = true;
                
                // 속도를 0으로 설정하여 완전히 멈추게 함
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
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
            }
        }
        // 공격 후 이동 처리
        else if (isMovingAfterAttack)
        {
            sr.flipX= true;
            transform.position = transform.position + new Vector3(-2f, 0f);
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

        }
    }
    
    private IEnumerator AttackForDuration(Vector3 direction)
    {
        isAttacking = true;
        float attackStartTime = Time.time;
        
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
        
        // 필요하다면 이동 애니메이션 설정
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
        }
        
        // 이 캐릭터(Doctor)도 같은 방향으로 이동하도록 설정
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // 기존 속도 초기화
        }
    }

    // 애니메이션 이벤트에서 호출할 수 있는 메서드 (선택 사항)
    public void OnAttackAnimationComplete()
    {
        // 공격 애니메이션이 완료된 후 실행할 코드
        Debug.Log("공격 애니메이션 완료");
        
        // 필요하다면 여기서 다른 상태로 전환하거나 추가 작업 수행
    }
}
