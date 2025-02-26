using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsJump = Animator.StringToHash("IsJump"); 
    // 문자를 해쉬를 사용하여 숫자열로 비교하는것이 용이

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();// 하위오브젝트까지 적용
    }

    public void Jump(Vector2 obj)
    {
        animator.SetBool(IsJump, obj.magnitude > 0.5f);//obj 는 벡터의 값(속도 방향 힘 등)을 의미
    }//magnitude는 벡터의 크기 obj가 0.5보다 크면 true(점프실행) 아니면 false
    
    public void Slide()
    {
        animator.SetBool(IsSlide, true);
    }

    //public void InvincibilityEnd()//무적시간 종료
    //{
    //    animator.SetBool(IsJump, false);
    //    animator.SetBool(IsSlide, false);
    //}

}
