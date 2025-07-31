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

    }

    private void UpdateFacingDirection()
    {
        if (target == null) return;

        // ���� ��
        if (target.position.x < transform.position.x)
        {
            characterRenderer.flipX = true;  // ���ʿ� ������ ���� �ٶ󺸰�
        }
        else
        {
            characterRenderer.flipX = false; // �����ʿ� ������ ������ �ٶ󺸰�
        }
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
        return Vector2.Distance(transform.position, target.position); // ���� 3 -> 2
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
            Debug.LogWarning("taget�� �����");
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        lookDirection = direction; // ���� �Ĵٺ���

        if (distance <= attackRange)
        {
            movementDirection = Vector2.zero;

            if (timeSinceLastAttack >= attackCooldown)
            {
                Attack();
            }

            return;
        }

        // movementDirection�� statHandler�� �ӵ��� �����ݴϴ�.
        if (statHandler != null)
        {
            movementDirection = direction * statHandler.Speed;
            Debug.Log($"[EnemyController] Moving towards: {movementDirection}, Speed: {statHandler.Speed}");
        }
        else
        {
            movementDirection = direction; // statHandler ������ �ϴ� ���� ���ͷ� �̵� (�����ų� �� ������)
            Debug.LogWarning("EnemyController: Stat Handler�� ���� �ӵ� ������ ������ �� �����ϴ�!");
        }

        UpdateFacingDirection(); // ���� ������Ʈ

    }

    private void Attack()
    {
        timeSinceLastAttack = 0f;
        animationHandler.Attack(); // Ʈ���� ������� ���� �ִϸ��̼� ����
    }

}
