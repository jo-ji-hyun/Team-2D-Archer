using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class BossBaseController : EnemyBaseController
{
    [Header("Base Settings")]
    public float maxHP = 1000f;
    protected float currentHP;
    public float BossAtkPower = 50f;
    public float moveSpeed = 2f;

    [Header("References")]
    protected Transform player;
    protected new SpriteRenderer characterRenderer;
    protected BossAnimationHandler bossAnimationHandler;

    // === HP바 호출 ===
    private BossUIHp _boss_UIHp;

    protected new virtual void Awake()
    {
        currentHP = maxHP;

        characterRenderer = GetComponentInChildren<SpriteRenderer>();
        bossAnimationHandler = GetComponent<BossAnimationHandler>();
    }

    public virtual void Init(Transform target)
    {
        player = target;
    }

    protected virtual void Update()
    {
        UpdateFacingDirection();

        if(_boss_UIHp == null)
        {
            _boss_UIHp = FindAnyObjectByType<BossUIHp>();
            _boss_UIHp.UpdateHP(currentHP, maxHP);
        }
    }

    protected void UpdateFacingDirection()
    {
        if (player == null || characterRenderer == null) return;

        characterRenderer.flipX = player.position.x < transform.position.x;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            _boss_UIHp.UpdateHP(0, maxHP); // 체력바 변동
            Die();
        }
        else
        {
            _boss_UIHp.UpdateHP(currentHP, maxHP); // 체력바 변동
        }
    }

    protected virtual void Die()
    {
        bossAnimationHandler.Death();
        StopAllCoroutines();
        Destroy(gameObject, 6f); // 6초 후 오브젝트 제거
    }

}
