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
        // "Player" 레이어 가져오기
        int playerLayer = LayerMask.NameToLayer("Player");

        // 씬에 있는 모든 GameObject를 검사
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == playerLayer)
            {
                target = obj.transform;
                break;
            }
        }
        // 공격 타이밍 시 데미지 전달 함수 등록
        animationHandler.OnAttackHit += DealDamageToTarget;
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
        animationHandler.Attack(); // 트리거 방식으로 공격 애니메이션 실행
    }

}
