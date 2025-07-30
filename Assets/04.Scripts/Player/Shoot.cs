using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // === �浹ü ���� === 
    [SerializeField] private string wallTag = "object"; // ��ֹ�
    [SerializeField] private LayerMask enemyLayer;       // ��

    private RangeWeapon _range_Weapon;

    private float _current_Duration;
    private Vector2 _direction;
    private bool _isReady;

    private Transform _pivot;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite_Renderer;

    private void Awake()
    {
        _sprite_Renderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _current_Duration += Time.deltaTime;

        if (_current_Duration > _range_Weapon.Duration)
        {
            DestroyShoot(transform.position, false);
        }

        _rigidbody2D.velocity = _direction * _range_Weapon.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag)) // ��ֹ�(Tag)�� �浹�� ����
        {
            DestroyShoot(transform.position, true);
        }
        if (enemyLayer.value == (enemyLayer.value | (1 << collision.gameObject.layer))) // ����(Layer)�� �浹�� ����
        {
            DestroyShoot(collision.ClosestPoint(transform.position), true);
            //
        }
    }


    public void Init(Vector2 direction, RangeWeapon weaponHandler)
    {
        _range_Weapon = weaponHandler;

        this._direction = direction;
        _current_Duration = 0;
        transform.localScale = Vector3.one * weaponHandler.magicSize;
        _sprite_Renderer.color = weaponHandler.magicColor;

        transform.right = this._direction;

        if (this._direction.x < 0)
            _pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            _pivot.localRotation = Quaternion.Euler(0, 0, 0);

        _isReady = true;
    }

    // === �ı� ���� ===
    private void DestroyShoot(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
