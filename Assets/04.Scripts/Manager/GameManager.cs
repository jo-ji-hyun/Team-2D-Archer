using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;

    public static GameManager Instance;
    public PlayerController player { get; private set; }

    // === �ڽĵ� ���� ===
    private EnemyManager _enemy_Manager;
    private StatsManager _stats_Manager;

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();

        
        _enemy_Manager = GetComponentInChildren<EnemyManager>();
        
        _stats_Manager = GetComponentInChildren<StatsManager>();

        player.Init(this, _stats_Manager);
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