using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController // �̵��� �ް� �ִϵ� ����ؾ���
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === CircleLayer�� ���� Ž�� ===
    private int _detected_Enemy;
    private int _enemy_Layer;

    // === ĳ���� �ִϸ��̼� ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager ����


    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // �÷��̾��� �ִϸ��̼� ���۳�Ʈ
        _camera = Camera.main;

        _enemy_Layer = LayerMask.NameToLayer("Enemy"); // Enemy ���̾ ����
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
        if(this._stats_Manager.stats.currentHP > 0 && _detected_Enemy > 0)
        {
            isAttacking = true;
            _animation_Player.AttackBehavior();
        }
        else if(this._stats_Manager.stats.currentHP > 0 || _detected_Enemy == 0)
        {
            isAttacking = false;
        }
    }

    // Ʈ���� �ȿ� �ٸ� �ݶ��̴��� ������ �� ����Ǵ� �Լ�
    private void OnTriggerStay2D(Collider2D other)
    {
        // "Enemy" ���̾ ���� ������Ʈ�� ���Դ��� Ȯ��
        if (other.gameObject.layer == _enemy_Layer)
        {
            // ���� �߰��ϸ� ����
            _detected_Enemy = 1;
        }
    }

    // Ʈ���ſ��� �ٸ� �ݶ��̴��� ������ �� ����Ǵ� �Լ�
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == _enemy_Layer)
        {
            // ���� ������ ����� ����
            _detected_Enemy = 0;

            if (_detected_Enemy <= 0)
            {
                _detected_Enemy = 0;
            }
        }
    }

    // === �����̱� ===
    void OnMove(InputValue inputValue)
    {
        if(this._stats_Manager.stats.currentHP > 0)
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
        SetPlayerAlive(this._stats_Manager.stats.currentHP); // BaseController�� hp�� �Ѱ���

        // === �ִϸ��̼� ��� ===
        if (this._stats_Manager.stats.currentHP > 0)
        {
            this._stats_Manager.TakeDamage((int)dmg);
            _animation_Player?.DamageSuffer();
        }
        else if (this._stats_Manager.stats.currentHP <= 0)
        {
            _animation_Player?.CharacterDie();
        }
        
    }
}
