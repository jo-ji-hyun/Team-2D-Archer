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

    // === �ڽĵ� ���� ===
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
        RoomIndex = 0; // 0���� ����
        StartWave(RoomIndex);
        fadeManager.ButtonOff();
    }

    // === ���̺�(��) ���� ===
    public void StartWave(int idx)
    {
        // �� ���� �� ������Ʈ�� �ִٸ� ����
        if (currentRoomObj != null)
            Destroy(currentRoomObj);

        _enemy_Manager.StartWave(idx);
        currentRoomObj = Instantiate(FieldManager.Instance.RoomPrefab, Vector3.zero, Quaternion.identity);
        currentRoomObj.name = "Room" + (idx + 1);

        // �� RoomManager.StartRoom() ȣ��!
        var roomMgr = currentRoomObj.GetComponent<RoomManager>();
        if (roomMgr != null)
            roomMgr.StartRoom();
        else
            Debug.LogError("RoomManager�� RoomPrefab�� ����!");
    }

    // === ���� ����(��) ���� ===
    public void StartNextWave()
    {
        RoomIndex++;
        StartWave(RoomIndex); // �� �� ������ ����!
    }

    // === �������� ����(�� Ŭ���� ��, ��ų ���� ��) ===
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