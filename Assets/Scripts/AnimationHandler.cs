using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsJump = Animator.StringToHash("IsJump"); 
    // ���ڸ� �ؽ��� ����Ͽ� ���ڿ��� ���ϴ°��� ����

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();// ����������Ʈ���� ����
    }

    public void Jump(Vector2 obj)
    {
        animator.SetBool(IsJump, obj.magnitude > 0.5f);//obj �� ������ ��(�ӵ� ���� �� ��)�� �ǹ�
    }//magnitude�� ������ ũ�� obj�� 0.5���� ũ�� true(��������) �ƴϸ� false
    
    public void Slide()
    {
        animator.SetBool(IsSlide, true);
    }

    //public void InvincibilityEnd()//�����ð� ����
    //{
    //    animator.SetBool(IsJump, false);
    //    animator.SetBool(IsSlide, false);
    //}

}
