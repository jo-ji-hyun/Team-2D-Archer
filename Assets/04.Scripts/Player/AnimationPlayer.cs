using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // === 애니메이션 트랜짓 bool값 불러오기 ===
    private static readonly int _isRun = Animator.StringToHash("isRun");
    private static readonly int _isAtk = Animator.StringToHash("isAtk");
    private static readonly int _isSuf = Animator.StringToHash("isSuf");
    private static readonly int _isDie = Animator.StringToHash("isDie");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // 자식에게서 가져옴
    }

    // === 움직임 ===
    public void Move()
    {
        animator.SetBool(_isRun, true); // 움직이고있는가?
    }

    // === 공격시 ===
    public void AttackBehavior()
    {
        animator.SetBool(_isAtk, true);
    }

    // === 피해를 입을시 ===
    public void DamageSuffer()
    {
        animator.SetBool(_isSuf, true);
    }

    // === 사망 ===
    public void CharacterDie()
    {
        animator.SetBool(_isDie, true);
    }
}
