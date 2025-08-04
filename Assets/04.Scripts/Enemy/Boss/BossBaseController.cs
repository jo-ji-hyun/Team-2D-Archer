using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class BossBaseController : MonoBehaviour
{
    [Header("Base Settings")]
    public float maxHP = 1000f;
    protected float currentHP;
    public float AtkPower = 50f;
    public float moveSpeed = 2f;

    [Header("References")]
    protected Transform player;
    protected SpriteRenderer characterRenderer;
    protected BossAnimationHandler animationHandler;

    [SerializeField] protected Transform target;

    // === HP�� ȣ�� ===
    private BossUIHp _boss_UIHp;

    protected virtual void Awake()
    {
        currentHP = maxHP;

        characterRenderer = GetComponentInChildren<SpriteRenderer>();
        animationHandler = GetComponent<BossAnimationHandler>();
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

        _boss_UIHp.UpdateHP(currentHP, maxHP); // ü�¹� ����

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animationHandler.Death();
        StopAllCoroutines();
        Destroy(gameObject, 6f); // 6�� �� ������Ʈ ����
    }

    // ������ �÷��̾ �ٶ󺸴� ������ ������Ʈ (���� ���� ���)
    protected Vector3 GetDirectionToPlayer()
    {
        if (player == null) return Vector3.zero;
        return (player.position - transform.position).normalized;
    }

}
