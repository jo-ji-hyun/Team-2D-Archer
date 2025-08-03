using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // === �ִϸ��̼� Ʈ���� bool�� �ҷ����� ===
    private static readonly int _isStay = Animator.StringToHash("isStay");
    private static readonly int _isRun = Animator.StringToHash("isMov");
    private static readonly int _isAtk = Animator.StringToHash("isAtk");
    private static readonly int _isDie = Animator.StringToHash("isDie");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // �ڽĿ��Լ� ������
    }

    // === ��� ===
    public void Stay()
    {
        animator.SetBool(_isStay, true);
        animator.SetBool(_isRun, false);
        animator.SetBool(_isAtk, false);
    }

    // === ������ ===
    public void Move()
    {
        animator.SetBool(_isRun, true);
        animator.SetBool(_isStay, false);
    }

    // === ���ݽ� ===
    public void AttackBehavior()
    {
        animator.SetBool(_isAtk,true);
        animator.SetBool(_isStay, false);
    }

    // === ��� ===
    public void CharacterDie()
    {
        animator.SetTrigger(_isDie);
        animator.SetBool(_isRun, false);
    }
}
