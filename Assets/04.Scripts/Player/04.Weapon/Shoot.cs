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

    // === ������ ó���� ���� �ҷ��� ===
    private StatsManager _stat_Manager;
   
    private void Awake()
    {
        _sprite_Renderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pivot = transform.GetChild(0);
        _stat_Manager = FindObjectOfType<StatsManager>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _current_Duration += Time.deltaTime;

        if (_current_Duration > _range_Weapon._magic_Codex.duration)
        {
            DestroyShoot(transform.position, false);
        }

        _rigidbody2D.velocity = _direction * _range_Weapon._magic_Codex.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer.value == (enemyLayer.value | (1 << collision.gameObject.layer))) // ����(Layer)�� �浹�� ����
        {
            DestroyShoot(collision.ClosestPoint(transform.position), true);
            // === ������ ü�� ��ȭ ===
            EnemyResourceController enemy = collision.GetComponent<EnemyResourceController>();
            float _total_Damage = FinalMagicDamage();

           // Debug.LogError($"{_total_Damage}"); // ������ Ȯ�ο�
            enemy.ChangeHealth(-_total_Damage);
        }
        else if (collision.gameObject.CompareTag(wallTag)) // ��ֹ�(Tag)�� �浹�� ����
        {
            DestroyShoot(transform.position, true);
        }

    }

    // === ���� ���� ������ ===
    private float FinalMagicDamage()
    {
        float currentDamage = _range_Weapon.Power;                      // ���� ������ 

        currentDamage += _range_Weapon._magic_Codex.Damage;

        // === �÷��̾� ���� ���� ===
        if (_stat_Manager.stats.attack >= 0)
        {
            currentDamage += _stat_Manager.stats.attack; 
        }

        return currentDamage;  // ���� ������ + ���� �⺻ ������ + �÷��̾� ����
    }

    // === ����ü ���� ����(ũ��, ����, ����) ===
    public void Init(Vector2 direction, RangeWeapon weaponHandler)
    {
        _range_Weapon = weaponHandler;

        this._direction = direction;
        _current_Duration = 0;
        transform.localScale = Vector3.one * _range_Weapon._magic_Codex.magicSize;

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
