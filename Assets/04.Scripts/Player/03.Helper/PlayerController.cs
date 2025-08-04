using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController // 이동을 받고 애니도 재생해야함
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === CircleLayer가 적을 탐지 ===
    private EnemyManager _enemy_Manager;

    // === 캐릭터 애니메이션 ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager 참조

    public PlayerStats stats; // 플레이어의 Stats

    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트
        _camera = Camera.main;

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SetMoveSpeed(_stats_Manager.stats.moveSpeed); // BaseController에 속도를 넘겨줌
    }

    public void Init(GameManager gameManager, StatsManager statsManager, EnemyManager enemyManager) // GamaManager와 StatsManager를 가져옴
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;
        this._enemy_Manager = enemyManager;
    }

    // === 플레이어 공격 로직 ===
    protected override void HandleAction()
    {
        if(_stats_Manager.stats.currentHP > 0 && _enemy_Manager.activeEnemies.Count > 0)
        {
            isAttacking = true;
            _animation_Player.AttackBehavior();
        }
        else if(_stats_Manager.stats.currentHP > 0 || _enemy_Manager.activeEnemies.Count <= 0)
        {
            isAttacking = false;
            _animation_Player.Stay();
        }
    }


    // === 움직이기 ===
    void OnMove(InputValue inputValue)
    {
        if(_stats_Manager.stats.currentHP > 0)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
            if (movementDirection.magnitude > .1f)
            {
                _animation_Player.Move();
            }
            else
            {
                _animation_Player.Stay();
            }
        }
        else
        {
            movementDirection = Vector2.zero;
        }

    }

    // === 방향 찾기 ===
    void OnLook(InputValue inputValue)
    {
        Vector3 mousePosition = inputValue.Get<Vector2>(); // 3D 값을 2D값으로 치환
        mousePosition.z = -10; // Z 값을 기존 위치인 -10으로 고정

        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    // === 데미지를 받을시 ===
    public void TakeDamage(float dmg)
    {
        SetPlayerAlive(_stats_Manager.stats.currentHP); // BaseController에 hp를 넘겨줌

        // === 애니메이션 재생 ===
        if (_stats_Manager.stats.currentHP > 0)
        {
            _stats_Manager.TakeDamage((int)dmg);
        }
        else if (_stats_Manager.stats.currentHP <= 0)
        {
            _animation_Player?.CharacterDie();
        }
        
    }

    public PlayerStats GetStats()
    {
        return stats;
    }
}
