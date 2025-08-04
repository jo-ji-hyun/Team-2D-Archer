using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour // === �⺻ ���⿡ ���ϰ� ===
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

    private static readonly int _isHit = Animator.StringToHash("IsHit");
    protected Animator animator;
    private bool _isDestroy = false; // ����ü ��Ȱ��ȭ

    private void Awake()
    {
        _sprite_Renderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pivot = transform.GetChild(0);

        animator = GetComponentInChildren<Animator>(); // �ڽĿ��Լ� ������
    }

    private void Update()
    {
        _current_Duration += Time.deltaTime;

        // === ����ü ���ӽð� ===
        if (_current_Duration > 5.0f)
        {
            StartCoroutine(nameof(DestroyAnimation));
        }
        else
        {
            // ===  �ӵ� ===
            _rigidbody2D.velocity = _direction * _range_Weapon._magic_Codex.speed;
        }
    }

    // === ����ü �浹 ���� ===
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer.value == (enemyLayer.value | (1 << collision.gameObject.layer)) && _isDestroy == false ) // ����(Layer)�� �浹�� ����
        {
            StartCoroutine(nameof(DestroyAnimation));
            // === ������ ü�� ��ȭ ===
            EnemyResourceController enemy = collision.GetComponent<EnemyResourceController>();
            float _total_Damage = FinalMagicDamage();

            //Debug.LogError($"{_total_Damage}"); // ������ Ȯ�ο�
            enemy.ChangeHealth(-_total_Damage);

            _skill_Manager._isSkill = false;
        }
        else if (collision.gameObject.CompareTag(wallTag)) // ��ֹ�(Tag)�� �浹�� ����
        {
            StartCoroutine(nameof(DestroyAnimation));
        }

    }

    // === ���� ���� ������ ===
    private float FinalMagicDamage()
    {
        float currentDamage = _range_Weapon.Power;                      // ���� ������ 

        currentDamage += _range_Weapon._magic_Codex.Damage;

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

    // === �ִϸ��̼��� ���� �ڷ�ƾ ===
    private IEnumerator DestroyAnimation()
    {
        _isDestroy = true;
        // === ���߰� �浹 ���� ===
        _rigidbody2D.velocity = Vector2.zero;

        animator.SetBool(_isHit, true);

        yield return new WaitForSeconds(1.0f);

        DestroyShoot(transform.position);
        animator.SetBool(_isHit, false);
    }

    // === �ı� ���� ===
    private void DestroyShoot(Vector3 position)
    {
        Destroy(this.gameObject);
    }
}
