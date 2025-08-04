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
    private BGMManager _bgm_Manager;

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

        _bgm_Manager = FindObjectOfType<BGMManager>();

        player.Init(this, _stats_Manager,_enemy_Manager);
        rangeWeapon.Init(_skill_Manager);

        ShootManager.Instance.GiveRange(_stats_Manager, _skill_Manager);
        SkillManager.Instance.GiveToSkillManager(_stats_Manager);

        statChoiceUI.SetUp(_stats_Manager, rangeWeapon);
    }

    private void Update()
    {
        if (gamestart)
        {
            bool allEnemiesCleared = _enemy_Manager.activeEnemies.Count == 0;
            bool isFinalStage = RoomIndex == 4;
            bool bossDead = BossManager.Instance == null || BossManager.Instance.CurrentBoss == null;

            if (allEnemiesCleared)
            {
                if (!isFinalStage || (isFinalStage && bossDead))
                {
                    RoomIndex++;
                    gamestart = false;
                    clear = 1;
                    Debug.Log("1");
                    SkillManager.Instance.ShowSkillChoice();

                    List<ChoiceData> randomChoices = GenerateRandomChoices();
                    statChoiceUI.ShowChoices(randomChoices);
                }
                
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
        _bgm_Manager.PlayBGM(1);
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

        // === 스탯 선택지 종류들 ===
        List<ChoiceData> baseStats = new List<ChoiceData>()
    {
        new ChoiceData { choiceType = ChoiceType.Stat, statType = StatType.Attack, value = 2 },
        new ChoiceData { choiceType = ChoiceType.Stat, statType = StatType.Defense, value = 1 },
        new ChoiceData { choiceType = ChoiceType.Stat, statType = StatType.MoveSpeed, value = 1f },
        new ChoiceData { choiceType = ChoiceType.Stat, statType = StatType.AttackSpeed, value = 0.5f },
        new ChoiceData { choiceType = ChoiceType.Stat, statType = StatType.HP, value = 20 },
    };

        // 중복 방지를 위해 복사
        allChoices.AddRange(baseStats);

        // === 스킬 중에서 아직 얻지 않은 것만 추가 ===
        foreach (Skill skill in SkillManager.Instance.allSkills)
        {
            if (skill == null) continue;

            bool alreadyHas = SkillManager.Instance.acquiredSkills.Exists(s => s.Index == skill.Index);
            if (!alreadyHas)
            {
                allChoices.Add(new ChoiceData
                {
                    choiceType = ChoiceType.Skill,
                    skill = skill,
                });
            }
        }

        List<ChoiceData> result = new List<ChoiceData>();
        int safety = 100;

        while (result.Count < 3 && allChoices.Count > 0 && safety-- > 0)
        {
            int idx = Random.Range(0, allChoices.Count);
            ChoiceData candidate = allChoices[idx];

            bool duplicate = result.Exists(x =>
                (x.choiceType == ChoiceType.Stat && candidate.choiceType == ChoiceType.Stat && x.statType == candidate.statType) ||
                (x.choiceType == ChoiceType.Skill && candidate.choiceType == ChoiceType.Skill && x.skill != null && candidate.skill != null && x.skill.Index == candidate.skill.Index)
            );

            if (!duplicate)
            {
                result.Add(candidate);
            }

            allChoices.RemoveAt(idx);
        }

        // === 결과 부족시 보충 ===
        while (result.Count < 3)
        {
            ChoiceData fallback = baseStats[Random.Range(0, baseStats.Count)];
            if (!result.Exists(x => x.statType == fallback.statType))
            {
                result.Add(fallback);
            }
        }

        return result;
    }
}

