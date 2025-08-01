using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // === 플레이어 스텟에서 정보를 가져옴 ===
    public PlayerStats stats = new PlayerStats();

    // === HP바 호출 ===
    private PlayerUIHelper _player_UIHelper;

    // ==== 게임매니저 호출 ====
    private GameManager _game_Manager;

    private void Awake()
    {
        _game_Manager = GetComponentInParent<GameManager>();
        // =========================

        // === UI핼퍼를 새롭게 만들기 ===
        _player_UIHelper = FindAnyObjectByType<PlayerUIHelper>();
    }


    private void Start()
    {
        stats.currentHP = stats.maxHP; // 체력 초기화, UI 갱신
        Hitpoint();
    }

    // === HP표시 ===
    public void Hitpoint()
    {
        _player_UIHelper.UpdateHP(stats.currentHP, stats.maxHP);
    }

    // === 레벨업 ===
    public void Levelup()
    {
        stats.level++;

        // === 레벨업 보너스 ===
        stats.maxHP += 10;                         
        stats.attack += 1f;                          
        stats.defense += 0.5f;
        
        stats.currentHP = stats.maxHP;                 
        Debug.Log($"레벨업! 현재 레벨: {stats.level}"); // ui 추가시 나중에 지우삼
        Hitpoint();
    }
    
    // === 플레이어가 데미지를 받을 시 ===
    public void TakeDamage(int dmg)
    {
        int realDamage = (int)Mathf.Max(0, dmg - stats.defense); // 데미지 계산
        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            Hitpoint();
            Debug.LogError("플레이어 사망");
            _game_Manager.GameOver();
        }
        else
        {
            Hitpoint(); 
        }    
    }

}
