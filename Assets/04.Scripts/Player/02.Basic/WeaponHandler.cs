using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // === ���� �ִϸ��̼� ���� & ȣ�� ===
    private static readonly int IsAttack = Animator.StringToHash("isAtk");

    // === ���� �ӵ� ===
    private float delay = 1.0f;
    public float Delay { get => delay; set => delay = value; }

    // === ���� ������ ===
    [SerializeField] private float weaponSize = 2.0f;
    public float Weaponsize { get => weaponSize; set => weaponSize = value; }

    // === �Ŀ� ===
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

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


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();          // �θ𿡰Լ� ������

        // === �̹��� ��������Ʈ�� �ڽĿ��� �־ ===
        _animator = GetComponentInChildren<Animator>();                
        _weapon_Renderer = GetComponentInChildren<SpriteRenderer>();   

        transform.localScale = Vector3.one * weaponSize;
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DecreaseAttackDelay(999f); // �׽�Ʈ��
        }
    }
   
    public virtual void Attack()
    {
        // === RangeWeapon���� ȣ�� ===
    }

    // === ���⵵ ���콺 ���⿡ ���� ������ ===
    public virtual void Rotate(bool isLeft)
    {
        _weapon_Renderer.flipY = isLeft;
    }

    // === ���ݼӵ� ���� �޼��� ===
    public void DecreaseAttackDelay(float amount)
    {
        delay -= amount; // delay���� �������� ��������

        if (delay < 0.5f)
        {
            delay = 0.5f; // �ּڰ�
        }
    }

}
