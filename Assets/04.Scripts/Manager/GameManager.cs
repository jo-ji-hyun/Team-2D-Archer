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
    public ShootManager shootManager { get; private set; }

    // === �ڽĵ� ���� ===
    private EnemyManager _enemy_Manager;
    private StatsManager _stats_Manager;
    private ShootManager _shoot_Manager;
    private SkillManager _skill_Manager;

    private GameObject currentRoomObj;

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();

        _enemy_Manager = GetComponentInChildren<EnemyManager>();
        _stats_Manager = GetComponentInChildren<StatsManager>();
        _shoot_Manager = GetComponentInChildren<ShootManager>();
        _skill_Manager = GetComponentInChildren<SkillManager>();

        // �÷��̾�, ���Ŵ��� �� ������ ����
        player.Init(this, _stats_Manager, _enemy_Manager);
        if (_shoot_Manager != null && _stats_Manager != null && _skill_Manager != null)
            _shoot_Manager.GiveRange(_stats_Manager, _skill_Manager);
    }

    private void Update()
    {
        if (_enemy_Manager.activeEnemies.Count == 0 )
        {
            Debug.Log("dd");
        }

        Debug.Log(_enemy_Manager.activeEnemies.Count);

    }

    // === ���� ����� ===

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        RoomIndex = 0;
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
        StartWave(RoomIndex);
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