using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsWalk");
    private static readonly int IsDamage = Animator.StringToHash("IsHit");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
   
    protected Animator animator;

    public System.Action OnAttackHit; // �ִϸ��̼ǿ��� ���� Ÿ�̹� ���� ���� �̺�Ʈ

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetTrigger(IsDamage);
    }

    public void Attack()
    {
        animator.SetTrigger(IsAttack);
    }

    public void Death()
    {
        animator.SetTrigger(IsDie);
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� �޼���
    public void InvokeAttackHit()
    {
        OnAttackHit?.Invoke();
    }
}
