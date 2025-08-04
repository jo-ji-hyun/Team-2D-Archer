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
    public EnemyStatHandler statHandler;

    [SerializeField] protected Transform target;
    [SerializeField] protected float AtkPower = 5f;

    protected EnemyManager enemyManager;

    protected bool isDead = false;

    public bool IsBoss { get; protected set; } = false;
    public bool IsDead => isDead; // 외부에서 읽기 전용 속성

    protected bool isAttacking;
    public float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<EnemyAnimationHandler>();
        statHandler = GetComponent<EnemyStatHandler>();

    }

    public virtual void Init(EnemyManager manager, Transform player)
    {
        this.enemyManager = manager;
        this.target = player;
    }

    protected virtual void DealDamageToTarget()
    {
        if (target == null) return;

        var playerHealth = target.GetComponent<PlayerController>();
        var EnemyAtk = AtkPower;
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(AtkPower);
        }

        isAttacking = false; // 공격 끝났다고 판단
    }

    protected virtual void FixedUpdate()
    {
        if (isDead || IsBoss) return; // 사망 시 추가행동 방지, 보스 추가명령 방지

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
        if (IsBoss) return; // 보스는 이동 로직 무시

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
        if (isDead) return;
        isDead = true;
        movementDirection = Vector2.zero;

        // 사망시 충돌범위 비활성화
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        _rigidbody.velocity = Vector3.zero;
        EnemyManager.Instance?.RemoveEnemy(this);
        animationHandler.Death();
        Destroy(gameObject, 3f);
    }
}
