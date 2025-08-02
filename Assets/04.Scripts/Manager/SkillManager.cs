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

    // === 스킬정보를 받아옴 ===
    public Skill skillData;

    public float _auto_Fire_Interval = 1.0f; // 자동 발사 간격 (초 단위)
    private float _auto_Fire_Timer = 0f; // 자동 발사 타이머

    private void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
        // 스킬을 추가함
        allSkills.Add(new Skill("FireBall", "화염구를 발사하여 적에게 피해를 준다.", 10f, 5f));
    }

    private void Update()
    {
        Skill fireballSkill = acquiredSkills.Find(s => s.skillName == "FireBall");
        //if (fireballSkill != null)
        //{
        //    autoFireTimer += Time.deltaTime; // 타이머 업데이트
        //    if (autoFireTimer >= autoFireInterval)
        //    {
        //        autoFireTimer = 0f; // 타이머 초기화

        //        GameObject playerObj = GameObject.FindWithTag("Player");
        //        if (playerObj != null)
        //           // UseSkill(fireballSkill, playerObj.transform.position);
        //    }
        //}
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
        }
        else
        {
            Debug.Log("획득 가능한 스킬이 없습니다.");
        }
    }

    public void UseSkill(string skill)//, Vector3 position)
    {
        Debug.Log($"{acquiredSkills} 스킬 사용!");

        //// 플레이어 위치에서 발사하도록 설정
        //GameObject playerObj = GameObject.FindWithTag("Player");
        //Vector3 spawnPos = playerObj != null ? playerObj.transform.position : position;

        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            Skill skillData = acquiredSkills[i];
            Debug.Log("스킬 번호 " + i + " 이름: " + skillData.skillName);

            float FireballDamage = Player.Instance.Stats.attack * 0.5f;
        }

        if (skill == null)
        {
            Debug.LogWarning("사용할 스킬이 null입니다.");
            return;
        }
    }
}



