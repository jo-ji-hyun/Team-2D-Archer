using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 싱글톤 패턴
    public static SkillManager Instance;

    // === 스킬 확인 ===
    public bool _isSkill;
    public float AbilityPower; // 스킬 데미지
    public float AbilitySpeed;  // 스킬 속도

    // === 다른 매니저 정의 ===
    private StatsManager _stats_Manager;

    private ShootManager _shoot_Manager;


    // 모든 스킬 목록
    public List<Skill> allSkills = new List<Skill>();

    // 플레이어가 보유한 스킬 목록
    public List<Skill> acquiredSkills = new List<Skill>();

    public GameObject fireballPrefab; // 파이어볼 프리팹
    public GameObject iceSpikePrefab;  // 아이스 스피어 프리팹
    public GameObject lightningBolt;   // 라이트볼 프리팹

    public GameObject skillButtonPrefab; // 스킬 버튼 프리팹 (UI)
    public Transform  skillButtonParent; // 스킬 버튼 부모 (UI)

    private void Awake()
    {
        Instance = this;

        // 모든 스킬 등록
        allSkills.Add(new Skill(0, "파이어 볼", "화염구를 발사하여 적에게 피해를 준다.", 10f, 5f, 2.0f, fireballPrefab));
        allSkills.Add(new Skill(1, "다크 스피어", "어둠 창을 발사하여 적을 얼린다.", 12f, 7f, 4.0f, iceSpikePrefab));
        allSkills.Add(new Skill(2, "라이트닝", "번개를 소환하여 적에게 피해를 준다.", 15f, 10f, 6.1f, lightningBolt));

    }
    void Update()
    {
        // === 매 프레임마다 모든 스킬의 쿨타임을 감소시킵니다. ===
        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            if (acquiredSkills[i].currentCoolTime > 0)
            {
                acquiredSkills[i].currentCoolTime -= Time.deltaTime;
            }
        }
    }

    // 새로운 스킬 획득
    public void AcquireSkill(Skill skill)
    {
        if (!acquiredSkills.Contains(skill))
        {
            acquiredSkills.Add(skill);
            CreateSkillButton(skill);
        }
    }

    // 스킬 선택 UI에 등장할 스킬 랜덤 3개 제공
    public void ShowSkillChoice()
    {
        if (SkillChoiceUI.Instance == null)
        {
            return;
        }
        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));
        List<Skill> choices = new List<Skill>();

        for (int i = 0; i < 3 && available.Count > 0; i++)
        {
            int idx = Random.Range(0, available.Count);
            choices.Add(available[idx]);
            available.RemoveAt(idx);
        }

        SkillChoiceUI.Instance.ShowChoices(choices);
    }

    // 남은 스킬 랜덤 획득
    public void AcquireRandomSkill()
    {
        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));
        if (available.Count > 0)
        {
            int rand = Random.Range(0, available.Count);
            Skill selected = available[rand];
            acquiredSkills.Add(selected);
            CreateSkillButton(selected);
        }
        else
        {
            Debug.Log("획득 가능한 스킬이 없습니다.");
        }
    }

    // 스킬 UI 버튼 생성 함수(확장용)
    public void CreateSkillButton(Skill skill)
    {
        if (skillButtonPrefab == null || skillButtonParent == null) return;

        GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI != null)
        {
            skillUI.Init(skill);
            skillUI.playerObj = GameObject.FindWithTag("Player");
        }
        else
        {
            return;
        }
    }

    // === 실제 스킬 발사 ===
    public void UseSkill(RangeWeapon range, Vector2 startPosition, Vector2 direction, int skillnum)
    {
        for (int i = 0; i < skillnum; i++)
        {
            int listIndex = skillnum - 1;

            var skillData = acquiredSkills[listIndex];

            GameObject magicPrefab = skillData.magicBulletPrefab;
            GameObject proj = Instantiate(magicPrefab, startPosition, Quaternion.identity);

            // === 최종계산을 위해 넘겨줌 ===
            AbilityPower = skillData.damage;
            AbilitySpeed = skillData.speed;

            MagicShoot magicShoot = proj.GetComponent<MagicShoot>();

            magicShoot.Init(direction, range, this._stats_Manager, this._shoot_Manager, this);
        }
    }

    // === 받은 매니저를 재정의 ===
    public void GiveToSkillManager(StatsManager stats)
    {
        this._stats_Manager = stats;
    }

}