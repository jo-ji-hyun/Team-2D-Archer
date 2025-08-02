using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // === 충돌체 정의 === 
    [SerializeField] private string wallTag = "object"; // 장애물
    [SerializeField] private LayerMask enemyLayer;       // 적

    // === Init으로 초기화 할 스크립트 ===
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

    // === 투사체 충돌 로직 ===
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer.value == (enemyLayer.value | (1 << collision.gameObject.layer))) // 몬스터(Layer)와 충돌시 삭제
        {
            DestroyShoot(collision.ClosestPoint(transform.position), true);
            // === 몬스터의 체력 변화 ===
            EnemyResourceController enemy = collision.GetComponent<EnemyResourceController>();
            float _total_Damage = FinalMagicDamage();

           // Debug.LogError($"{_total_Damage}"); // 데미지 확인용
            enemy.ChangeHealth(-_total_Damage);
        }
        else if (collision.gameObject.CompareTag(wallTag)) // 장애물(Tag)과 충돌시 삭제
        {
            DestroyShoot(transform.position, true);
        }

    }

    // === 최종 마법 데미지 ===
    private float FinalMagicDamage()
    {
        float currentDamage = _range_Weapon.Power;                      // 무기 데미지 

        currentDamage += _range_Weapon._magic_Codex.Damage;

        // === 플레이어 스텟 참조 ===
        if (_stats_Manager.stats.attack >= 0)
        {
            currentDamage += _stats_Manager.stats.attack; 
        }

        return currentDamage;  // 무기 데미지 + 마법 기본 데미지 + 플레이어 스텟
    }

    // === 투사체 정보 설정(크기, 방향) ===
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

    // === 파괴 로직 ===
    private void DestroyShoot(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
