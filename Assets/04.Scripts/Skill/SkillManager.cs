using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Diagnostics.Contracts;

public class SkillManager : MonoBehaviour
{
    // 싱글톤 패턴으로 접근하기 위한 스태틱 인스턴스
    public static SkillManager Instance;

    // 모든 스킬을 저장하는 리스트
    public List<Skill> allSkills = new List<Skill>();

    // 현재 플레이어가 보유중인 스킬 리스트
    public List<Skill> acquiredSkills = new List<Skill>();

    private void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
        // 테스트 용 기본 스킬
        allSkills.Add(new Skill("Flamethrower", "강력한 화염 공격을 가한다."));
    }

    // 새로운 스킬을 플레이어에게 부여하는 함수
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // 스킬을 획득한 스킬 목록에 추가
        Debug.Log($"스킬 획득 : {skill.skillName}"); // 실제 효과 적용 로직은 따로 분리 가능함.
    }

    public void AcquireRandomSkill()
    {
        // 아직 획득 하지 않은 스킬 중에서만 랜덤으로 선택.
        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));

        if (available.Count > 0)
        {
            int rand = Random.Range(0, available.Count);
            Skill selected = available[rand];
            acquiredSkills.Add(selected);
            Debug.Log($"스킬 획득: {selected.skillName} - {selected.description}");
        }
        else
        {
            Debug.Log("획득 가능한 스킬이 없습니다.");
        }
    }

    // 이미 획득한 스킬 목록을 보여주는 함수.
    public void ShowAcquiredSkills()
    {
        Debug.Log("==보유 스킬==");
        foreach (var s in acquiredSkills)
        {
            Debug.Log($"{s.skillName} : {s.description}");
        }
    }

    public void UseSkill(Skill skill, Vector3 position)
    {
        Debug.Log($"{skill.skillName} 스킬 사용! 위치: {position}");
    }
}



