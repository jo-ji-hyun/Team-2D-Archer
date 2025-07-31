using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;

    public static GameManager Instance;
    public PlayerController player { get; private set; }

    private EnemyManager _enemy_Manager;

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        
        _enemy_Manager = GetComponentInChildren<EnemyManager>();
    }

    // === ���� ����� ===
    private void Start()
    {
        StartGame(); // �ӽ�
    }

    // === ���� ���� ===
    public void StartGame()
    {
        _enemy_Manager.StartWave(1);
    }

    // === ���� ���� ===
    void StartNextWave()
    {

    }

    // === �������� ���� ===
    public void EndOfWave()
    {
        StartNextWave();
    }

    // === �÷��̾� ����� ===
    public void GameOver()
    {
        _enemy_Manager.StopWave();
        // ���ξ����� ���ư��� (���Ŀ� �߰�)
    }

}