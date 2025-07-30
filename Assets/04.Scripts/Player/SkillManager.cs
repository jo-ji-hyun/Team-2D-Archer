using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Diagnostics.Contracts;

public class Skillmanager : MonoBehaviour
{
    // 싱글톤 패턴으로 접근하기 위한 스태틱 인스턴스
    public static Skillmanager Instance;

    // 모든 스킬을 저장하는 리스트
    public List<Skill> allSkills = new List<Skill>();

    // 현재 플레이어가 보유중인 스킬 리스트
    public List<Skill> acquiredSkills = new List<Skill>();

    public SkillExecutor executor; // 스킬 사용을 위한 실행기

    private void Awake()
    {
        if (Instance == null) Instance = this; // 싱글톤 인스턴스 설정
    }

    // 새로운 스킬을 플레이어에게 부여하는 함수
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // 스킬을 획득한 스킬 목록에 추가
        Debug.Log($"스킬 획득 : {skill.skillName}"); // 실제 효과 적용 로직은 따로 분리 가능함.
    }

    // 아직 획득하지 않은 스킬 중에서 랜덤으로 N개 추출
    public List<Skill> GetRandomSkills(int count)
    {
        List<Skill> candidates = new List<Skill>();

        foreach (Skill skill in allSkills)
        {
            if (!acquiredSkills.Contains(skill))
                candidates.Add(skill);
        }


        // 랜덤하게 N개 뽑기
        List<Skill> randomSkills = new List<Skill>();
        for (int i = 0; i < count && candidates.Count > 0; i++)
        {
            int Index = Random.Range(0, candidates.Count);
            randomSkills.Add(candidates[Index]);
            candidates.RemoveAt(Index);
        }

        return randomSkills;
    }

    // 스킬 사용(이펙트 실행)
    public void UseSkill(string skillName, Vector3 position)
    {
        Skill skill = acquiredSkills.Find(s => s.skillName == skillName);
        if (skill != null)
        {
            executor.skillPrefab = skill.skillPrefab;
            executor.Use(position);
        }
    }
}



