using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D; // Rigidbody2D  불러오기

    // === 캐릭터,무기 캡슐화 === 
    [SerializeField] private SpriteRenderer character_Renderer;
    [SerializeField] private Transform weapon_Pivot;

    // === 움직임 제어 ===
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    [SerializeField] private float _baseMovement = 3; // 기본 속도

    // === 마우스에 따라 바라보는 방향 제어 ===
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // === 캐릭터 애니메이션 ===
    protected AnimationPlayer animationPlayer;

    // === 무기 프리팹 준비 ===
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking; 
    private float _time_Since_Last_Attack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        // 컴퍼넌트에서 정보를 가져옴
        animationPlayer = GetComponent<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트

        // === 무기 프리팹에서 무기 가져오기 ===
        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weapon_Pivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        HandleAction();
        HandleAttackDelay();
    }

    protected virtual void Update()
    {
        Rotate(lookDirection);
    }

    // === 기본적인 이동 ===
    private void Movement(Vector2 direction)
    {
        direction = direction * _baseMovement;

        _rigidbody2D.velocity = direction;
        animationPlayer.Move(direction);     // 이동 애니메이션
    }

    // === 공격 ===
    protected virtual void HandleAction()
    {
        
    }

    // === 마우스 위치에 따라 바라보는 방향 변경 ===
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        character_Renderer.flipX = isLeft;

        if (weapon_Pivot != null) // 무기를 들었을 경우
        {
            weapon_Pivot.rotation = Quaternion.Euler(0, 0, rotZ); // 무기 조준 방향

            // === 지팡이(무기)가 캐릭터 뒤에 있도록 ===
            float _weapon_Xpos = Mathf.Abs(0.45f);
            float targetLocalX = isLeft ? _weapon_Xpos : -_weapon_Xpos;
            weapon_Pivot.localPosition = new Vector3(targetLocalX, weapon_Pivot.localPosition.y, weapon_Pivot.localPosition.z);
        }

        weaponHandler?.Rotate(isLeft); // 무기도 캐릭에 맞춰 회전함
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        if (_time_Since_Last_Attack <= weaponHandler.Delay)
        {
            _time_Since_Last_Attack += Time.deltaTime;
        }

        if (isAttacking && _time_Since_Last_Attack > weaponHandler.Delay)
        {
            _time_Since_Last_Attack = 0;
            AttackCall();
        }
    }

    protected virtual void AttackCall()
    {

        if (lookDirection != Vector2.zero)
        {
            weaponHandler?.Attack(); // 무기들었으면 공격해라
            animationPlayer.AttackBehavior();
        }

    }

}

