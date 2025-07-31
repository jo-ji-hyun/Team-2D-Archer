using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera _camera;
    private StatsManager _stats_Manager; // StatsManager ����

    public void Init(GameManager gameManager)
    {
        base.Awake();

        this.gameManager = gameManager;
        _camera = Camera.main;

        _stats_Manager = FindObjectOfType<StatsManager>(); // StatsManager�� ã��

        SetMoveSpeed(_stats_Manager.stats.moveSpeed); // BaseController�� �ӵ��� �Ѱ���
    }

    // === �÷��̾� ���� ���� ===
    protected override void HandleAction()
    {
        if(_stats_Manager.stats.currentHP != 0)
        {
            // ���߿� �� Ž���� �������� ���� ... �����
            isAttacking = true;
        }

    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
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
}
