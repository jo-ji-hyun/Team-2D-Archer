using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] protected SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected EnemyAnimationHandler animationHandler;
    protected EnemyStatHandler statHandler;

    [SerializeField] protected Transform target;
    [SerializeField] protected float AtkPower = 5f;

    private PlayerController playerController;

    protected bool isAttacking;
    public float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<EnemyAnimationHandler>();
        statHandler = GetComponent<EnemyStatHandler>();

    }

    protected virtual void DealDamageToTarget()
    {
        if (target == null) return;

        var playerHealth = target.GetComponent<PlayerController>();
        var EnemyAtk = AtkPower;
        if (playerHealth != null)
        {
            playerController.TakeDamage(AtkPower);
        }

        isAttacking = false; // 공격 끝났다고 판단
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
        HandleAction(); // 이걸 호출해야함
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;

        animationHandler.Move(direction);
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;
        animationHandler.Death();
        Destroy(gameObject, 2f);
    }
}
