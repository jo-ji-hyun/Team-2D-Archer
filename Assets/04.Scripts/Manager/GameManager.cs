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

    private GameObject currentRoomObj;

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();

        _enemy_Manager = GetComponentInChildren<EnemyManager>();
        _stats_Manager = GetComponentInChildren<StatsManager>();

        player.Init(this, _stats_Manager);
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        RoomIndex = 0; // 0부터 시작
        StartWave(RoomIndex);
        fadeManager.ButtonOff();
    }

    // === 웨이브(방) 시작 ===
    public void StartWave(int idx)
    {
        // ★ 이전 방 오브젝트가 있다면 제거
        if (currentRoomObj != null)
            Destroy(currentRoomObj);

        _enemy_Manager.StartWave(idx);
        currentRoomObj = Instantiate(FieldManager.Instance.RoomPrefab, Vector3.zero, Quaternion.identity);
        currentRoomObj.name = "Room" + (idx + 1);

        // ★ RoomManager.StartRoom() 호출!
        var roomMgr = currentRoomObj.GetComponent<RoomManager>();
        if (roomMgr != null)
            roomMgr.StartRoom();
        else
            Debug.LogError("RoomManager가 RoomPrefab에 없음!");
    }

    // === 다음 던전(방) 진행 ===
    public void StartNextWave()
    {
        RoomIndex++;
        StartWave(RoomIndex); // ★ 방 생성도 같이!
    }

    // === 스테이지 종료(방 클리어 후, 스킬 선택 후) ===
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