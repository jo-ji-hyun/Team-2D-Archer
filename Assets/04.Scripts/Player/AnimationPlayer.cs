using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // === �ִϸ��̼� Ʈ���� bool�� �ҷ����� ===
    private static readonly int _isRun = Animator.StringToHash("isRun");
    private static readonly int _isAtk = Animator.StringToHash("isAtk");
    private static readonly int _isDie = Animator.StringToHash("isDie");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // �ڽĿ��Լ� ������
    }

    // === ������ ===
    public void Move(Vector2 obj)
    {
        animator.SetBool(_isRun, obj.magnitude > .5f); // �����̰��ִ°�?
    }

    // === ���ݽ� === (���� �ִϸ��̼��� ������ ������ .... ����� )
    public void AttackBehavior()
    {
        animator.SetBool(_isAtk, true);
    }

    // === ��� ===
    public void CharacterDie()
    {
        animator.SetBool(_isDie, true);
    }
}
