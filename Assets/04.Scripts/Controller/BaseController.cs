using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D; // Rigidbody2D  �ҷ�����

    // === ĳ����,���� ĸ��ȭ === 
    [SerializeField] private SpriteRenderer character_Renderer;
    [SerializeField] private Transform weapon_Pivot;

    // === ������ ���� ===
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    [SerializeField] private float _baseMovement = 3; // �⺻ �ӵ�

    // === ���콺�� ���� �ٶ󺸴� ���� ���� ===
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // === ĳ���� �ִϸ��̼� ===
    protected AnimationPlayer animationPlayer;

    // === ���� ������ �غ� ===
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking; 
    private float _time_Since_Last_Attack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        // ���۳�Ʈ���� ������ ������
        animationPlayer = GetComponent<AnimationPlayer>();  // �÷��̾��� �ִϸ��̼� ���۳�Ʈ

        // === ���� �����տ��� ���� �������� ===
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

    // === �⺻���� �̵� ===
    private void Movement(Vector2 direction)
    {
        direction = direction * _baseMovement;

        _rigidbody2D.velocity = direction;
        animationPlayer.Move(direction);     // �̵� �ִϸ��̼�
    }

    // === ���� ===
    protected virtual void HandleAction()
    {
        
    }

    // === ���콺 ��ġ�� ���� �ٶ󺸴� ���� ���� ===
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        character_Renderer.flipX = isLeft;

        if (weapon_Pivot != null) // ���⸦ ����� ���
        {
            weapon_Pivot.rotation = Quaternion.Euler(0, 0, rotZ); // ���� ���� ����

            // === ������(����)�� ĳ���� �ڿ� �ֵ��� ===
            float _weapon_Xpos = Mathf.Abs(0.45f);
            float targetLocalX = isLeft ? _weapon_Xpos : -_weapon_Xpos;
            weapon_Pivot.localPosition = new Vector3(targetLocalX, weapon_Pivot.localPosition.y, weapon_Pivot.localPosition.z);
        }

        weaponHandler?.Rotate(isLeft); // ���⵵ ĳ���� ���� ȸ����
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
            weaponHandler?.Attack(); // ���������� �����ض�
            animationPlayer.AttackBehavior();
        }

    }

}

