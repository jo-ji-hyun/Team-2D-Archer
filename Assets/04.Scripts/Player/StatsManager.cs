using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // === 플레이어 스텟에서 정보를 가져옴 ===
    public PlayerStats stats = new PlayerStats();

    // ==== 게임매니저 호출 ====
    private GameManager _game_Manager;

    private void Awake()
    {
        _game_Manager = GetComponentInParent<GameManager>();
    }
    // =========================

    private void Start()
    {
        stats.currentHP = stats.maxHP; // 체력 초기화, UI 갱신
    }

    public void Levelup()
    {
        if (stats.currentHP <= 0) // 혹시 모르니
        {
            _game_Manager.GameOver(); // 게임오버 호출
        }

        stats.level++;

        // === 레벨업 보너스 ===
        stats.maxHP += 10f;                         
        stats.attack += 2f;                          
        stats.defense += 1;
        
        stats.currentHP = stats.maxHP;                 
        Debug.Log($"레벨업! 현재 레벨: {stats.level}"); // 나중에 지우삼
    }
    
    // === 플레이어가 데미지를 받을 시 ===
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - stats.defense); // 데미지 계산

        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            _game_Manager.GameOver();
        }

        Debug.Log($"데미지 받음: {realDamage}, 현재 남은 HP: {stats.currentHP}"); // 나중에 삭제
    }

}
