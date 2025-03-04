using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsSlide = Animator.StringToHash("IsSlide");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsSlideHit = Animator.StringToHash("IsSlideHit");
    private static readonly int IsJumpHit = Animator.StringToHash("IsJumpHit");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    // ���ڸ� �ؽ��� ����Ͽ� ���ڿ��� ���ϴ°��� ����
    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();// ����������Ʈ���� ����
    }
    public void Jump(bool isJumping)
    {
        animator.SetBool(IsJump, isJumping);
    }
    public void Slide(bool isSliding)
    {
        animator.SetBool(IsSlide, isSliding);
    }
    public void SlideHit()
    {
        animator.SetBool(IsSlideHit, true);
        Invoke(nameof(ResetSlideHit), 0.5f);
    }
    public void JumpHit()
    {
        animator.SetBool(IsJumpHit, true);
        Invoke(nameof(ResetJumpHit), 0.5f);
    }
    public void Hit()
    {
        animator.SetBool(IsHit, true);
        Invoke(nameof(ResetHit), 0.5f);
    }
    public void ResetAnim()// �ִϸ��̼� �ʱ�ȭ
    {
        animator.SetBool(IsJump, false);
        animator.SetBool(IsSlide, false);
        animator.SetBool(IsSlideHit, false);
        animator.SetBool(IsJumpHit, false);
        animator.SetBool(IsHit, false);
    }
    private void ResetSlideHit() => animator.SetBool(IsSlideHit, false);
    private void ResetJumpHit() => animator.SetBool(IsJumpHit, false);
    private void ResetHit() => animator.SetBool(IsHit, false);
}