using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === 캐릭터 애니메이션 ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager 참조

    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트
        _camera = Camera.main;
    }
    public void Init(GameManager gameManager, StatsManager statsManager) // GamaManager와 StatsManager를 가져옴
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;

        SetMoveSpeed(this._stats_Manager.stats.moveSpeed); // BaseController에 속도를 넘겨줌
    }

    // === 플레이어 공격 로직 ===
    protected override void HandleAction()
    {
        if(this._stats_Manager.stats.currentHP > 0)
        {
            // 나중에 적 탐지시 공격으로 변경 ... 고민중
            isAttacking = true;
            _animation_Player.AttackBehavior();
        }
        else
        {
            isAttacking = false;
        }
    }

    void OnMove(InputValue inputValue)
    {
        if(this._stats_Manager.stats.currentHP > 0)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
            _animation_Player.Move();     // 이동 애니메이션
        }

    }

    // === 방향 찾기 ===
    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
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
        
        // === 애니메이션 재생 ===
        if (this._stats_Manager.stats.currentHP > 0)
        {
            this._stats_Manager.TakeDamage(dmg);
            _animation_Player?.DamageSuffer();
        }
        else if (this._stats_Manager.stats.currentHP <= 0)
        {
            _animation_Player?.CharacterDie();
        }
        
    }
}
