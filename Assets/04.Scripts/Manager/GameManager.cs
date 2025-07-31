using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;

    public static GameManager Instance;
    public PlayerController player { get; private set; }

    private StatsManager _stats_Manager;
    private EnemyManager _enemy_Manager;

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        
        _enemy_Manager = GetComponentInChildren<EnemyManager>();

        _stats_Manager = player.GetComponent<StatsManager>();
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