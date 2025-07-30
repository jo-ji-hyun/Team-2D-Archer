using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // === 충돌체 정의 === (나중에 수정)
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeapon _range_Weapon;

    private float _current_Duration;
    private Vector2 _direction;
    private bool _isReady;

    private Transform _pivot;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite_Renderer;

    public bool fxOnDestory = true;

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
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyShoot(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory);
        }
        else if (_range_Weapon.target.value == (_range_Weapon.target.value | (1 << collision.gameObject.layer)))
        {
            DestroyShoot(collision.ClosestPoint(transform.position), fxOnDestory);
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

    // === 파괴 로직 ===
    private void DestroyShoot(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
