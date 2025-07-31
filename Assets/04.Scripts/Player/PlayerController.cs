using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera _camera;
    private StatsManager _stats_Manager; // StatsManager 참조

    public void Init(GameManager gameManager)
    {
        base.Awake();

        this.gameManager = gameManager;
        _camera = Camera.main;

        _stats_Manager = FindObjectOfType<StatsManager>();
        if (_stats_Manager == null)
        {
            Debug.LogError("PlayerController: 씬에서 StatsManager 컴포넌트를 찾을 수 없습니다! (GameManager 하위에 StatsManager가 있는지 확인)");
        }
        else
        {
            SetMoveSpeed(_stats_Manager.stats.moveSpeed);
        }
    }

    // === 플레이어 공격 로직 ===
    protected override void HandleAction()
    {
        // 나중에 적 탐지시 공격으로 변경 ... 고민중
        isAttacking = true;
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
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
}
