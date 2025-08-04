using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button startButton;

    public static int clear = 0;

    public static bool gamestart = false;

    public static int RoomIndex = 0;

    public static bool famestart = false;

    public GameObject playertarget;

    public static GameManager Instance;

    public FadeManager fadeManager;
    public PlayerController player { get; private set; }

    public RangeWeapon rangeWeapon { get; private set; }

    [SerializeField] private StatChoiceUI statChoiceUI;

    // === 매니저들 들고옴 ===
    private EnemyManager _enemy_Manager;
    private StatsManager _stats_Manager;
    private ShootManager _shoot_Manager;
    private SkillManager _skill_Manager;
    private RoomManager _room_Manager;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        rangeWeapon = FindAnyObjectByType<RangeWeapon>();

        // === 자식한테서 찾음 ===
        _enemy_Manager = GetComponentInChildren<EnemyManager>();
        _stats_Manager = GetComponentInChildren<StatsManager>();
        _shoot_Manager = GetComponentInChildren<ShootManager>();
        _skill_Manager = GetComponentInChildren<SkillManager>();
        _room_Manager = GetComponentInChildren<RoomManager>();

        player.Init(this, _stats_Manager,_enemy_Manager);
        rangeWeapon.Init(_skill_Manager);

        ShootManager.Instance.GiveRange(_stats_Manager, _skill_Manager);
        SkillManager.Instance.GiveToSkillManager(_stats_Manager);

        statChoiceUI.SetUp(player.stats, player.GetComponentInChildren<WeaponHandler>());
    }

    private void Update()
    {
        if (gamestart)
        {
            if (_enemy_Manager.activeEnemies.Count == 0)
            {
                RoomIndex++;
                gamestart = false;
                clear = 1;
                Debug.Log("1");
                _room_Manager.OnRoomCleared();

                List<ChoiceData> randomChoices = GenerateRandomChoices();
                statChoiceUI.ShowChoices(randomChoices);
            }
        }
        else
        {
            if (_enemy_Manager.activeEnemies.Count != 0)
            {
                gamestart = true;
                Debug.Log("2");
            }
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
        _enemy_Manager.StartWave(RoomIndex);
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

    // === 게임오버시 캐릭터를 사라지게 함 ===
    private IEnumerator PlayerDeathRoutine()
    {
        yield return new WaitForSeconds(2.0f);       

        playertarget.SetActive(false);
    }

    private List<ChoiceData> GenerateRandomChoices()
    {
        List<ChoiceData> allChoices = new List<ChoiceData>();

        allChoices.Add(new ChoiceData { choiceType = ChoiceType.Stat, name = "공격력 +2", statType = StatType.Attack, value = 2 });
        allChoices.Add(new ChoiceData { choiceType = ChoiceType.Stat, name = "방어력 +2", statType = StatType.Defense, value = 1 });
        allChoices.Add(new ChoiceData { choiceType = ChoiceType.Stat, name = "이동속도 +0.5", statType = StatType.MoveSpeed, value = 0.05f });
        allChoices.Add(new ChoiceData { choiceType = ChoiceType.Stat, name = "공격속도 +0.1", statType = StatType.AttackSpeed, value = 0.05f });
        allChoices.Add(new ChoiceData { choiceType = ChoiceType.Skill, name = "HP +20", statType = StatType.HP, value = 20 });

        foreach (Skill skill in SkillManager.Instance.allSkills)
        {
            allChoices.Add(new ChoiceData
            {
                choiceType = ChoiceType.Skill,
                skill = skill,
                name = skill.skillName,
            });
        }

        List<ChoiceData> result = new List<ChoiceData>();
        while (result.Count < 3 && allChoices.Count > 0)
        {
            int index = Random.Range(0, allChoices.Count);
            result.Add(allChoices[index]);
            allChoices.RemoveAt(index);
        }

        return result;
    }
}

