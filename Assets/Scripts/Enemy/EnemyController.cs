using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

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

}
