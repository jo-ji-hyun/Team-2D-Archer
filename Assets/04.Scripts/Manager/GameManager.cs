using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int RoomIndex;
    public Button startButton;

    public static int clear = 0;

    public static bool famestart = false;

    public GameObject playertarget;

    public static GameManager Instance;

    public FadeManager fadeManager;
    public PlayerController player { get; private set; }

    public RangeWeapon rangeWeapon { get; private set; }

    // === �Ŵ����� ���� ===
    private EnemyManager _enemy_Manager;
    private StatsManager _stats_Manager;
    private ShootManager _shoot_Manager;
    private SkillManager _skill_Manager;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        rangeWeapon = FindAnyObjectByType<RangeWeapon>();

        // === �ڽ����׼� ã�� ===
        _enemy_Manager = GetComponentInChildren<EnemyManager>();
        _stats_Manager = GetComponentInChildren<StatsManager>();
        _shoot_Manager = GetComponentInChildren<ShootManager>();
        _skill_Manager = GetComponentInChildren<SkillManager>();

        player.Init(this, _stats_Manager,_enemy_Manager);
        rangeWeapon.Init(_skill_Manager);

        ShootManager.Instance.GiveRange(_stats_Manager, _skill_Manager); 
    }

    private void Update()
    {

        if (_enemy_Manager.activeEnemies.Count == 0 )
        {
            Debug.Log(_enemy_Manager.activeEnemies.Count);
        }

    }



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


    public void StartWave()
    {
        _enemy_Manager.StartWave(0);
    }

    void StartNextWave()
    {

    }


    public void EndOfWave()
    {
        // StartNextWave();
    }


    public void GameOver()
    {
        StartCoroutine(PlayerDeathRoutine());
        _enemy_Manager.StopWave();
    }

    // === ���ӿ����� ĳ���͸� ������� �� ===
    private IEnumerator PlayerDeathRoutine()
    {
        yield return new WaitForSeconds(2.0f);       

        playertarget.SetActive(false);
    }

}