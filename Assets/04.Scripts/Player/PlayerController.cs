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

        _stats_Manager = FindObjectOfType<StatsManager>(); // StatsManager를 찾음

        SetMoveSpeed(_stats_Manager.stats.moveSpeed); // BaseController에 속도를 넘겨줌
    }

    // === 플레이어 공격 로직 ===
    protected override void HandleAction()
    {
        if(_stats_Manager.stats.currentHP != 0)
        {
            // 나중에 적 탐지시 공격으로 변경 ... 고민중
            isAttacking = true;
        }

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
