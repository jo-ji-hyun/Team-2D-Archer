using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === ĳ���� �ִϸ��̼� ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager ����

    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // �÷��̾��� �ִϸ��̼� ���۳�Ʈ
        _camera = Camera.main;
    }
    public void Init(GameManager gameManager, StatsManager statsManager) // GamaManager�� StatsManager�� ������
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;

        SetMoveSpeed(this._stats_Manager.stats.moveSpeed); // BaseController�� �ӵ��� �Ѱ���
    }

    // === �÷��̾� ���� ���� ===
    protected override void HandleAction()
    {
        if(this._stats_Manager.stats.currentHP > 0)
        {
            // ���߿� �� Ž���� �������� ���� ... �����
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
            _animation_Player.Move();     // �̵� �ִϸ��̼�
        }

    }

    // === ���� ã�� ===
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

    // === �������� ������ ===
    public void TakeDamage(float dmg)
    {
        
        // === �ִϸ��̼� ��� ===
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
