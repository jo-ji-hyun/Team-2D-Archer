using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;
    public Button startButton;

    public static int clear = 0;

    public static GameManager Instance;
    public FadeManager fadeManager;
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

        player.Init(this, _stats_Manager,_enemy_Manager); // 플레이어한테 매니저를 넣어줌
    }

    private void Update()
    {
        if (_enemy_Manager.activeEnemies.Count == 0 )
        {

        }

    }

    // === 던전 입장시 ===
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        Instantiate(FieldManager.Instance.RoomPrefab, new Vector3(0, 0, 0), Quaternion.identity).name = "Room";
    }

    public void StartGame()
    {
        StartWave();
        fadeManager.ButtonOff();
    }

    // === 게임 시작 ===
    public void StartWave()
    {
        _enemy_Manager.StartWave(0);
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