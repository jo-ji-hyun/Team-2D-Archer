using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // === 공격 애니메이션 지정 & 호출 ===
    private static readonly int IsAttack = Animator.StringToHash("isAtk");

    // === 공격 속도 ===
    private float delay = 1.0f;
    public float Delay { get => delay; set => delay = value; }

    // === 무기 사이즈 ===
    [SerializeField] private float weaponSize = 2.0f;
    public float Weaponsize { get => weaponSize; set => weaponSize = value; }

    // === 파워 ===
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

    // === 무기 범위 ===
    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    // === 타깃을 정확하게 설정하기 위해서 ===
    public LayerMask target;

    // === 가져오기 ===
    private Animator _animator;               // 공격 애니메이션
    private SpriteRenderer _weapon_Renderer;   // 무기 이미지

    // === 캐릭터 이동과 비교하기 위해 ===
    public BaseController Controller { get; private set; }


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();          // 부모에게서 가져옴

        // === 이미지 스프라이트는 자식에게 있어서 ===
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
            DecreaseAttackDelay(999f); // 테스트용
        }
    }
   
    public virtual void Attack()
    {
        // === RangeWeapon에서 호출 ===
    }

    // === 무기도 마우스 방향에 따라 뒤집기 ===
    public virtual void Rotate(bool isLeft)
    {
        _weapon_Renderer.flipY = isLeft;
    }

    // === 공격속도 증가 메서드 ===
    public void DecreaseAttackDelay(float amount)
    {
        delay -= amount; // delay값이 작을수록 빠른공격

        if (delay < 0.5f)
        {
            delay = 0.5f; // 최솟값
        }
    }

}
