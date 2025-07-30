using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // === ���� �ִϸ��̼� ���� & ȣ�� ===
    private static readonly int IsAttack = Animator.StringToHash("isAtk");

    // === ���̵��� ���� ���� ������ ===
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    // === ���� ������ ===
    [SerializeField] private float weaponSize = 1.0f;
    public float Weaponsize { get => weaponSize; set => weaponSize = value; }

    // === �Ŀ� ===
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

    // === ���ǵ� ===
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    // === ���� ���� ===
    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    // === Ÿ���� ��Ȯ�ϰ� �����ϱ� ���ؼ� ===
    public LayerMask target;

    // === �������� ===
    private Animator _animator;               // ���� �ִϸ��̼�
    private SpriteRenderer _weapon_Renderer;   // ���� �̹���

    // === ĳ���� �̵��� ���ϱ� ���� ===
    public BaseController Controller { get; private set; }

    // === �˹� === (���߿� �߰�)
    //[SerializeField] private bool isOnKnockback = false;
    //public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    //[SerializeField] private float knockbackPower = 0.1f;
    //public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    //[SerializeField] private float knockbackTime = 0.5f;
    //public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();          // �θ𿡰Լ� ������

        // === �̹��� ��������Ʈ�� �ڽĿ��� �־ ===
        _animator = GetComponentInChildren<Animator>();                
        _weapon_Renderer = GetComponentInChildren<SpriteRenderer>();   

        transform.localScale = Vector3.one * weaponSize;
    }
    
    // === ���ݽ� �ִϸ��̼� ������ ===
    public virtual void Attack()
    {
        AttackAnimation();
    }

    // === ���� �ִϸ��̼� ===
    public void AttackAnimation()
    {
        _animator.SetTrigger(IsAttack);
    }

    // === ���⵵ ���콺 ���⿡ ���� ������ ===
    public virtual void Rotate(bool isLeft)
    {
        _weapon_Renderer.flipY = isLeft;
    }
}
