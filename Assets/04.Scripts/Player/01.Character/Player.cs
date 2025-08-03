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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // LevelUP(); // 레벨업(테스트용)
        }
    }

    // 레벨업
    public void LevelUP()
    {
        if (Stats.currentHP <= 0)
        {
            return; // 플레이어가 죽으면 레벨업을 못하게 막음.
        }

        Stats.level++;
        Stats.maxHP += 10; // 레벨업 시 최대 HP 증가
        Stats.attack += 2f; // 레벨업 시 공격력 증가
        Stats.defense += 1f; // 레벨업 시 방어력 증가
        Stats.currentHP = Stats.maxHP; // 레벨업 시 현재 HP를 최대 HP로 회복
        Debug.Log($"레벨업! 현재 레벨: {Stats.level}");
    }
}
