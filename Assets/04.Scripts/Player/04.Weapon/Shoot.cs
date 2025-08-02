using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // === �浹ü ���� === 
    [SerializeField] private string wallTag = "object"; // ��ֹ�
    [SerializeField] private LayerMask enemyLayer;       // ��

    // === Init���� �ʱ�ȭ �� ��ũ��Ʈ ===
    private RangeWeapon _range_Weapon;
    private StatsManager _stats_Manager;
    private ShootManager _shoot_Manager;
    private SkillManager _skill_Manager;

    private float _current_Duration;
    private Vector2 _direction;

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
        _current_Duration += Time.deltaTime;

        if (_current_Duration > _range_Weapon._magic_Codex.duration)
        {
            DestroyShoot(transform.position, false);
        }

        _rigidbody2D.velocity = _direction * _range_Weapon._magic_Codex.speed;
    }

    // === ����ü �浹 ���� ===
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
        if (_stats_Manager.stats.attack >= 0)
        {
            currentDamage += _stats_Manager.stats.attack; 
        }

        return currentDamage;  // ���� ������ + ���� �⺻ ������ + �÷��̾� ����
    }

    // === ����ü ���� ����(ũ��, ����) ===
    public void Init(Vector2 direction, RangeWeapon range, StatsManager statsManager, ShootManager shootManager, SkillManager skillManager)
    {
        _range_Weapon = range;
       this._stats_Manager = statsManager;
       this._shoot_Manager = shootManager;
       this._skill_Manager = skillManager;

        this._direction = direction;
        _current_Duration = 0;
        transform.localScale = Vector3.one * _range_Weapon._magic_Codex.magicSize;

        transform.right = this._direction;

        if (this._direction.x < 0)
            _pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            _pivot.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // === �ı� ���� ===
    private void DestroyShoot(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
