using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsWalk");
    private static readonly int IsSkill = Animator.StringToHash("IsSkill");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    protected Animator animator;

    public System.Action OnAttackHit; // 애니메이션에서 공격 타이밍 전달 받을 이벤트

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move()
    {
        animator.SetBool(IsMoving, true);
    }

    public void Idle()
    {
        animator.SetBool(IsMoving, false);
    }

    public void Skill()
    {
        animator.SetTrigger(IsSkill);
    }

    public void Attack()
    {
        animator.SetTrigger(IsAttack);
    }

    public void Death()
    {
        animator.SetTrigger(IsDie);
    }

    // 애니메이션 이벤트에서 호출될 메서드
    public void InvokeAttackHit()
    {
        OnAttackHit?.Invoke();
    }
}
