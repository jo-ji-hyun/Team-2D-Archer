using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController // �̵��� �ް� �ִϵ� ����ؾ���
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === CircleLayer�� ���� Ž�� ===
    private EnemyManager _enemy_Manager;

    // === ĳ���� �ִϸ��̼� ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager ����

    public PlayerStats stats; // �÷��̾��� Stats

    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // �÷��̾��� �ִϸ��̼� ���۳�Ʈ
        _camera = Camera.main;

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SetMoveSpeed(_stats_Manager.stats.moveSpeed); // BaseController�� �ӵ��� �Ѱ���
    }

    public void Init(GameManager gameManager, StatsManager statsManager, EnemyManager enemyManager) // GamaManager�� StatsManager�� ������
    { 
        this._game_Manager = gameManager;
        this._stats_Manager = statsManager;
        this._enemy_Manager = enemyManager;
    }

    // === �÷��̾� ���� ���� ===
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


    // === �����̱� ===
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

    // === ���� ã�� ===
    void OnLook(InputValue inputValue)
    {
        Vector3 mousePosition = inputValue.Get<Vector2>(); // 3D ���� 2D������ ġȯ
        mousePosition.z = -10; // Z ���� ���� ��ġ�� -10���� ����

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
        SetPlayerAlive(_stats_Manager.stats.currentHP); // BaseController�� hp�� �Ѱ���

        // === �ִϸ��̼� ��� ===
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
