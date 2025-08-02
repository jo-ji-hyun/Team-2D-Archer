using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 싱글톤 패턴
    public static SkillManager Instance;

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
        allSkills.Add(new Skill(0, "FireBall", "화염구를 발사하여 적에게 피해를 준다.", 10f, 5f, fireballPrefab));
        allSkills.Add(new Skill(1, "IceSpike", "얼음 창을 발사하여 적을 얼린다.", 8f, 7f, iceSpikePrefab));
        allSkills.Add(new Skill(2, "LightningBolt", "번개를 소환하여 적에게 피해를 준다.", 12f, 10f, lightningBolt));

        acquiredSkills.Add(new Skill(0, "FireBall", "화염구를 발사하여 적에게 피해를 준다.", 10f, 5f, fireballPrefab)); // 확인용추가
    }

    // 새로운 스킬 획득
    public void AcquireSkill(Skill skill)
    {
        if (!acquiredSkills.Contains(skill))
        {
            acquiredSkills.Add(skill);
            Debug.Log($"스킬 획득 : {skill.skillName}");
            CreateSkillButton(skill);
        }
    }

    // 스킬 선택 UI에 등장할 스킬 랜덤 3개 제공
    public void ShowSkillChoice()
    {
        if (SkillChoiceUI.Instance == null)
        {
            Debug.LogError("SkillChoiceUI 인스턴스가 없습니다. SkillChoiceUI를 씬에 추가하세요.");
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
            Debug.Log($"스킬 획득: {selected.skillName} - {selected.description}");
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

        Debug.Log("[Skill]스킬 버튼 생성: " + skill.skillName);
        GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI != null)
        {
            skillUI.Init(skill);
            skillUI.playerObj = GameObject.FindWithTag("Player");
        }
        else
        {
            Debug.LogWarning("SkillUI 스크립트가 프리팹에 없음");
        }
    }

    // === 실제 스킬 발사 ===
    public void UseSkill(Vector2 startPosition, Vector2 direction, int skillnum)
    {
        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            if (skillnum == acquiredSkills[i].Index)
            {
                GameObject magicPrefab = acquiredSkills[i].magicBulletPrefab;
                GameObject proj = Instantiate(magicPrefab, startPosition, Quaternion.identity);
                Debug.Log("스킬 발동");
            }

        }
    }

   
}