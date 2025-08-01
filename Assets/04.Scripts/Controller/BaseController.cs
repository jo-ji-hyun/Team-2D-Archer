using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D; // Rigidbody2D  �ҷ�����

    // === ĳ����,���� ĸ��ȭ === 
    [SerializeField] private SpriteRenderer character_Renderer;
    [SerializeField] private Transform weapon_Pivot;

    // === ĳ���� ���� ����
    protected bool _current_Player;

    // === ������ ���� ===
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    protected float _currentMoveSpeed;

    // === ���콺�� ���� �ٶ󺸴� ���� ���� ===
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // === ���� ������ �غ� ===
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking; 
    private float _time_Since_Last_Attack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        // ���۳�Ʈ���� ������ ������

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
        if(_current_Player == true)
        {
            Rotate(lookDirection);
        }
    }

    // === PlayerController���� ü�¸� Ȯ���� ===
    public void SetPlayerAlive(int hp)
    {
        // _current_Player = (hp > 0f) ? true : false;
        _current_Player = (hp > 0);
    }

    // === PlayerController���� �ӵ��� ������ ===
    public void SetMoveSpeed(float speed)
    {
        _currentMoveSpeed = speed;
    }

    // === �⺻���� �̵� ===
    private void Movement(Vector2 direction)
    {
        direction = direction * _currentMoveSpeed;

        _rigidbody2D.velocity = direction;
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

    // === ���� ������ ===
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

    // === ���ݽ� ���� ���� ���� Ȯ�� ===
    protected virtual void AttackCall()
    {
        if (lookDirection != Vector2.zero)
        {
            weaponHandler?.Attack(); // ���������� �����ض�
        }
    }
}

