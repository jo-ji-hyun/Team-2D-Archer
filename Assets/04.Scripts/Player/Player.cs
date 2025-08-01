using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerStats Stats = new PlayerStats();

    public int level = 1; // 플레이어 레벨

    void Start()
    {
        Stats.currentHP = Stats.maxHP; // 체력 초기화, UI 갱신
        Debug.Log("HP: " + Stats.currentHP + ", ATK: " + Stats.attack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUP(); 
        } // 레벨업(테스트용)

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f);
        } // 대미지 받기(테스트용)

        if (Input.GetKeyDown(KeyCode.J))
        {
            Stats.currentHP = 0;
        } // 즉사 대미지(테스트용)
    }
    // 데미지 받기
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - Stats.defense);
        Stats.currentHP -= realDamage;
        if (Stats.currentHP < 0) Stats.currentHP = 0; // HP가 0 이하로 떨어지면 사망.
        Debug.Log($"데미지 받음: {realDamage}, 현재 남은 HP: {Stats.currentHP}");
    }

    // 레벨업
    public void LevelUP()
    {
        if (Stats.currentHP <= 0)
        {
            Debug.Log("플레이어가 사망했습니다."); 
            return; // 플레이어가 죽으면 레벨업을 못하게 막음.
        }

        Stats.level++;
        Stats.maxHP += 10f; // 레벨업 시 최대 HP 증가
        Stats.attack += 2f; // 레벨업 시 공격력 증가
        Stats.defense += 1f; // 레벨업 시 방어력 증가
        Stats.currentHP = Stats.maxHP; // 레벨업 시 현재 HP를 최대 HP로 회복
        Debug.Log($"레벨업! 현재 레벨: {Stats.level}");

        if (Stats.level == 2)
        {
            Skill fireballSkill = SkillManager.Instance.allSkills.Find(s => s.skillName == "FireBall");
            if (fireballSkill != null && !SkillManager.Instance.acquiredSkills.Contains(fireballSkill))
            {
                SkillManager.Instance.AcquireSkill(fireballSkill);
                Debug.Log("파이어볼 스킬을 획득했습니다");
            }
        }
    }
}
