using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController // 이동을 받고 애니도 재생해야함
{
    private GameManager _game_Manager;
    private Camera _camera;

    // === CircleLayer가 적을 탐지 ===
    private int _detected_Enemy;
    private int _enemy_Layer;

    // === 캐릭터 애니메이션 ===
    private AnimationPlayer _animation_Player;

    private StatsManager _stats_Manager; // StatsManager 참조


    protected override void Awake()
    {
        base.Awake();
        _animation_Player = GetComponent<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트
        _camera = Camera.main;

        _enemy_Layer = LayerMask.NameToLayer("Enemy"); // Enemy 레이어를 저장
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

    // 트리거 안에 다른 콜라이더가 들어왔을 때 실행되는 함수
    private void OnTriggerStay2D(Collider2D other)
    {
        // "Enemy" 레이어를 가진 오브젝트가 들어왔는지 확인
        if (other.gameObject.layer == _enemy_Layer)
        {
            // 적을 발견하면 증가
            _detected_Enemy = 1;
        }
    }

    // 트리거에서 다른 콜라이더가 나갔을 때 실행되는 함수
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == _enemy_Layer)
        {
            // 적이 범위를 벗어나면 감소
            _detected_Enemy = 0;

            if (_detected_Enemy <= 0)
            {
                _detected_Enemy = 0;
            }
        }
    }

    // === 움직이기 ===
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
        SetPlayerAlive(this._stats_Manager.stats.currentHP); // BaseController에 hp를 넘겨줌

        // === 애니메이션 재생 ===
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
