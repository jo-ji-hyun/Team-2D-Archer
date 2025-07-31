using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;

    public static GameManager Instance;
    public PlayerController player { get; private set; }

    // === 자식들 참조 ===
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

    // === 던전 입장시 ===
    private void Start()
    {
        StartGame(); // 임시
    }

    // === 게임 시작 ===
    public void StartGame()
    {
        _enemy_Manager.StartWave(1);
    }

    // === 다음 던전 ===
    void StartNextWave()
    {

    }

    // === 스테이지 종료 ===
    public void EndOfWave()
    {
        StartNextWave();
    }

    // === 플레이어 사망시 ===
    public void GameOver()
    {
        _enemy_Manager.StopWave();
        // 메인씬으로 돌아가기 (추후에 추가)
    }

}