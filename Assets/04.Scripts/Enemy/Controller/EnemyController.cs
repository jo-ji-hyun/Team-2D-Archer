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

        // 방향 비교
        if (target.position.x < transform.position.x)
        {
            characterRenderer.flipX = true;  // 왼쪽에 있으면 왼쪽 바라보게
        }
        else
        {
            characterRenderer.flipX = false; // 오른쪽에 있으면 오른쪽 바라보게
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();  // 중요: HandleAction 호출 포함
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position); // 수정 3 -> 2
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
            Debug.LogWarning("taget이 비었음");
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        lookDirection = direction; // 먼저 쳐다보기

        if (distance <= attackRange)
        {
            movementDirection = Vector2.zero;

            if (timeSinceLastAttack >= attackCooldown)
            {
                Attack();
            }

            return;
        }

        // movementDirection에 statHandler의 속도를 곱해줍니다.
        if (statHandler != null)
        {
            movementDirection = direction * statHandler.Speed;
            Debug.Log($"[EnemyController] Moving towards: {movementDirection}, Speed: {statHandler.Speed}");
        }
        else
        {
            movementDirection = direction; // statHandler 없으면 일단 단위 벡터로 이동 (느리거나 안 움직임)
            Debug.LogWarning("EnemyController: Stat Handler가 없어 속도 정보를 가져올 수 없습니다!");
        }

        UpdateFacingDirection(); // 방향 업데이트

    }

    private void Attack()
    {
        timeSinceLastAttack = 0f;
        animationHandler.Attack(); // 트리거 방식으로 공격 애니메이션 실행
    }

}
