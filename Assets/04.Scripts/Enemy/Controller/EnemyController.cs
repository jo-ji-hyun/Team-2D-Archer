using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBaseController
{
    private EnemyManager enemyManager;

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    protected virtual void Start()
    {
        // "Player" ���̾� ��������
        int playerLayer = LayerMask.NameToLayer("Player");

        // ���� �ִ� ��� GameObject�� �˻�
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == playerLayer)
            {
                target = obj.transform;
                break;
            }
        }
        // ���� Ÿ�̹� �� ������ ���� �Լ� ���
        animationHandler.OnAttackHit += DealDamageToTarget;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();  // �߿�: HandleAction ȣ�� ����
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        if (distance <= attackRange)
        {
            movementDirection = Vector2.zero;
            lookDirection = direction;

            if (!isAttacking && timeSinceLastAttack >= attackCooldown)
            {
                Attack();
            }

            return;
        }

        lookDirection = direction;
        movementDirection = direction;

    }

    private void Attack()
    {
        isAttacking = true;
        timeSinceLastAttack = 0f;
        animationHandler.Attack(); // Ʈ���� ������� ���� �ִϸ��̼� ����
    }

}
