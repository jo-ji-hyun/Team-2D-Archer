using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D; // Rigidbody2D  �ҷ�����

    // === ĳ����,���� ĸ��ȭ === 
    [SerializeField] private SpriteRenderer _characterRenderer;
    [SerializeField] private Transform _weaponPivot;

    // === ������ ���� ===
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    [SerializeField] private float _baseMovement = 3; // �⺻ �ӵ�

    // === ���콺�� ���� �ٶ󺸴� ���� ���� ===
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // ���۳�Ʈ���� ������ ������

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    // === �⺻���� �̵� ===
    private void Movement(Vector2 direction)
    {
        direction = direction * _baseMovement;

        _rigidbody2D.velocity = direction;
    }

    protected virtual void HandleAction()
    {

    }

    // === ���콺 ��ġ�� ���� �ٶ󺸴� ���� ���� ===
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _characterRenderer.flipX = isLeft;

        if (_weaponPivot != null) // ���⸦ ����� ���
        {
            _weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}

