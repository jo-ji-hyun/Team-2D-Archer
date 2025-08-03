using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // === 애니메이션 트랜짓 bool값 불러오기 ===
    private static readonly int _isStay = Animator.StringToHash("isStay");
    private static readonly int _isRun = Animator.StringToHash("isMov");
    private static readonly int _isAtk = Animator.StringToHash("isAtk");
    private static readonly int _isDie = Animator.StringToHash("isDie");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // 자식에게서 가져옴
    }

    // === 대기 ===
    public void Stay()
    {
        animator.SetBool(_isStay, true);
        animator.SetBool(_isRun, false);
        animator.SetBool(_isAtk, false);
    }

    // === 움직임 ===
    public void Move()
    {
        animator.SetBool(_isRun, true);
        animator.SetBool(_isStay, false);
    }

    // === 공격시 ===
    public void AttackBehavior()
    {
        animator.SetBool(_isAtk,true);
        animator.SetBool(_isStay, false);
    }

    // === 사망 ===
    public void CharacterDie()
    {
        animator.SetTrigger(_isDie);
        animator.SetBool(_isRun, false);
    }
}
