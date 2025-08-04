using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot: MonoBehaviour //  === ���� ������Ʈ�� ���ϰ� ===
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

        // === ����ü ���ӽð� ===
        if (_current_Duration > 5.0f)
        {
            DestroyShoot(transform.position);
        }

        // === �ӵ� ===
        _rigidbody2D.velocity = _direction * _skill_Manager.AbilitySpeed;

    }

    // === ����ü �浹 ���� ===
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer.value == (enemyLayer.value | (1 << collision.gameObject.layer))) // ����(Layer)�� �浹�� ����
        {
            DestroyShoot(transform.position);

            EnemyTypes(collision);
        }
        else if (collision.gameObject.CompareTag(wallTag)) // ��ֹ�(Tag)�� �浹�� ����
        {
            DestroyShoot(transform.position);
        }

    }

    // === �Ϲ� �� / ���� �� �Ǻ� ===
    public void EnemyTypes(Collider2D collision)
    {
        float _total_Damage = FinalMagicDamage();

        // === ������ ü�� ��ȭ ===
        EnemyResourceController enemy = collision.GetComponent<EnemyResourceController>();

        // Debug.LogError($"{_total_Damage}"); // ������ Ȯ�ο�
        if (enemy != null)
        {
            enemy.ChangeHealth(-_total_Damage);
        }

        BossBaseController boss = collision.GetComponent<BossBaseController>();
        if (boss != null)
        {
            boss.TakeDamage(_total_Damage);
        }
    }

    // === ���� ���� ������ ===
    private float FinalMagicDamage()
    {
        float currentDamage = _range_Weapon.Power;                      // ���� ������ 

        currentDamage += _skill_Manager.AbilityPower; 

        // === �÷��̾� ���� ���� ===

        currentDamage += _stats_Manager.stats.attack;

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
    private void DestroyShoot(Vector3 position)
    {
        Destroy(this.gameObject);
    }
}
