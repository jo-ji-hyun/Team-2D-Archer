using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === ĳ���� �ִϸ��̼� ===
    private AnimationPlayer animationPlayer;

    private StatsManager _stats_Manager; // StatsManager ����

    protected override void Awake()
    {
        base.Awake();
        animationPlayer = GetComponent<AnimationPlayer>();  // �÷��̾��� �ִϸ��̼� ���۳�Ʈ
        _camera = Camera.main;
    }
    public void Init(GameManager gameManager, StatsManager statsManager)
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;

        SetMoveSpeed(this._stats_Manager.stats.moveSpeed); // BaseController�� �ӵ��� �Ѱ���
    }

    // === �÷��̾� ���� ���� ===
    protected override void HandleAction()
    {
        if(this._stats_Manager.stats.currentHP != 0)
        {
            // ���߿� �� Ž���� �������� ���� ... �����
            isAttacking = true;
            animationPlayer.AttackBehavior();
        }
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
        animationPlayer.Move();     // �̵� �ִϸ��̼�
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
        this._stats_Manager.TakeDamage(dmg);
        // === �ִϸ��̼� ��� ===
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
