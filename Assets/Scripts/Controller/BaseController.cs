using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D; // Rigidbody2D  불러오기

    // === 캐릭터,무기 캡슐화 === 
    [SerializeField] private SpriteRenderer _characterRenderer;
    [SerializeField] private Transform _weaponPivot;

    // === 움직임 제어 ===
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    [SerializeField] private float _baseMovement = 3; // 기본 속도

    // === 마우스에 따라 바라보는 방향 제어 ===
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // 컴퍼넌트에서 정보를 가져옴

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

    // === 기본적인 이동 ===
    private void Movement(Vector2 direction)
    {
        direction = direction * _baseMovement;

        _rigidbody2D.velocity = direction;
    }

    protected virtual void HandleAction()
    {

    }

    // === 마우스 위치에 따라 바라보는 방향 변경 ===
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _characterRenderer.flipX = isLeft;

        if (_weaponPivot != null) // 무기를 들었을 경우
        {
            _weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}

