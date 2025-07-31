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

    public GameObject fireballPrefab; // 파이어볼 프리팹
    public GameObject skillButtenPrefeb; // 스킬 버튼 프리팹
    public Transform skillButtenParent; // 스킬 버튼을 배치할 부모 오브젝트

    private void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
        // 테스트 용 기본 스킬
        allSkills.Add(new Skill("Flamethrower", "강력한 화염 공격을 가한다."));
        allSkills.Add(new Skill("FireBall", "화염구를 발사하여 적에게 피해를 준다."));
    }

    // 새로운 스킬을 플레이어에게 부여하는 함수
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // 스킬을 획득한 스킬 목록에 추가
        Debug.Log($"스킬 획득 : {skill.skillName}"); // 실제 효과 적용 로직은 따로 분리 가능함.

        CreateSkillButten(skill); // UI에 스킬 버튼 생성
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
            CreateSkillButten(selected);
        }
        else
        {
            Debug.Log("획득 가능한 스킬이 없습니다.");
        }
    }
    public void CreateSkillButten(Skill skill)
    {
        Debug.Log("[Skill]스킬 버튼 생성: " + skill.skillName);

        GameObject btnObj = Instantiate(skillButtenPrefeb, skillButtenParent);
        Debug.Log("[Skill] 버튼 실제 생성됨: " + btnObj.name);

        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI == null)
            Debug.LogWarning("SkillUI 스크립트가 프리팹에 없음");

        else
            Debug.Log("SkillUI 연결 완료");

        skillUI.Init(skill); // 스킬 UI 초기화
        skillUI.playerObj = GameObject.FindWithTag("Player"); // 플레이어 오브젝트를 찾아서 할당


        // 만약 init 함수 없이 직접 할당 할려면:
        // skillUI.skillData = skill;
        // skillUI.skillNameText.text = skill.skillName; // 스킬 이름 설정
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

        // 플레이어 위치에서 발사하도록 설정
        GameObject playerObj = GameObject.FindWithTag("Player");
        Vector3 spawnPos = playerObj != null ? playerObj.transform.position : position;

        if (skill.skillName == "FireBall")
        {
            Vector2 dir = Vector2.right; // 예시로 오른쪽 방향으로 발사

            GameObject proj = Instantiate(fireballPrefab, position, Quaternion.identity);

            proj.GetComponent<Projectile>().SetDirection(dir); // 발사체 방향 설정
        }
    }


}



