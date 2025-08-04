using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : EnemyBaseController
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float fireCooldown = 2f;

    private float lastFireTime = -999f;

    // Init �޼��� �������̵�
    public override void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected override void FixedUpdate()
    {
        if (isDead) return;

        base.FixedUpdate(); // base���� Movement + HandleAction ����
    }

    protected override void HandleAction()
    {
        if (target == null)
        {
            Debug.LogWarning("[EnemyRangedController] Ÿ���� ��� ����");
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        lookDirection = direction;

        if (distance <= attackRange)
        {
            movementDirection = Vector2.zero;

            if (Time.time - lastFireTime >= fireCooldown)
            {
                Fire();
            }
        }
        else
        {
            // �̵� (��, ��Ÿ� ���� ����)
            if (statHandler != null)
            {
                movementDirection = direction * statHandler.Speed;
            }
            else
            {
                movementDirection = direction;
            }
        }

        UpdateFacingDirection();
    }

    private void Fire()
    {
        lastFireTime = Time.time;

        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("[EnemyRangedController] projectilePrefab�̳� firePoint�� �Ҵ���� ����");
            return;
        }

        Vector3 dir = (target.position - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        EnemyProjectile projectile = proj.GetComponent<EnemyProjectile>();
        if (projectile != null)
        {
            projectile.direction = dir;
            projectile.damage = this.AtkPower;
        }

        animationHandler.Attack(); // ���� �ִϸ��̼� Ʈ����
    }

    private void UpdateFacingDirection()
    {
        if (target == null || characterRenderer == null) return;

        characterRenderer.flipX = target.position.x < transform.position.x;
    }

    private float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    private Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
}
