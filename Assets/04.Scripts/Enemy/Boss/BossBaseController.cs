using System.Collections;
using System.Collections.Generic;
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
            Die();
        }
    }

    protected virtual void Die()
    {
        animationHandler?.Death();
        StopAllCoroutines();
        Destroy(gameObject, 2f); // 2초 후 오브젝트 제거
    }

    // 보스가 플레이어를 바라보는 방향을 업데이트 (보스 공용 기능)
    protected Vector3 GetDirectionToPlayer()
    {
        if (player == null) return Vector3.zero;
        return (player.position - transform.position).normalized;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}
