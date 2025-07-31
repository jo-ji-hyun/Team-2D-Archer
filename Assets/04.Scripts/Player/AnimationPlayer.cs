using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // === �ִϸ��̼� Ʈ���� bool�� �ҷ����� ===
    private static readonly int _isRun = Animator.StringToHash("isRun");
    private static readonly int _isAtk = Animator.StringToHash("isAtk");
    private static readonly int _isSuf = Animator.StringToHash("isSuf");
    private static readonly int _isDie = Animator.StringToHash("isDie");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // �ڽĿ��Լ� ������
    }

    // === ������ ===
    public void Move()
    {
        animator.SetBool(_isRun, true); // �����̰��ִ°�?
    }

    // === ���ݽ� ===
    public void AttackBehavior()
    {
        animator.SetBool(_isAtk, true);
    }

    // === ���ظ� ������ ===
    public void DamageSuffer()
    {
        animator.SetBool(_isSuf, true);
    }

    // === ��� ===
    public void CharacterDie()
    {
        animator.SetBool(_isDie, true);
    }
}
