using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === 캐릭터 애니메이션 ===
    private AnimationPlayer animationPlayer;

    private StatsManager _stats_Manager; // StatsManager 참조

    protected override void Awake()
    {
        base.Awake();
        animationPlayer = GetComponent<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트
        _camera = Camera.main;
    }
    public void Init(GameManager gameManager, StatsManager statsManager)
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;

        SetMoveSpeed(this._stats_Manager.stats.moveSpeed); // BaseController에 속도를 넘겨줌
    }

    // === 플레이어 공격 로직 ===
    protected override void HandleAction()
    {
        if(this._stats_Manager.stats.currentHP != 0)
        {
            // 나중에 적 탐지시 공격으로 변경 ... 고민중
            isAttacking = true;
            animationPlayer.AttackBehavior();
        }
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
        animationPlayer.Move();     // 이동 애니메이션
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
        this._stats_Manager.TakeDamage(dmg);
        // === 애니메이션 재생 ===
        if (this._stats_Manager.stats.currentHP > 0)
        {
            animationPlayer?.DamageSuffer();
        }
        else if (this._stats_Manager.stats.currentHP <= 0)
        {
            animationPlayer?.CharacterDie();
        }

    }
}
