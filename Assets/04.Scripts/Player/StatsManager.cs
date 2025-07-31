using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // === 플레이어 스텟에서 정보를 가져옴 ===
    public PlayerStats stats = new PlayerStats();

    // === 캐릭터 애니메이션 ===
    protected AnimationPlayer animationPlayer;

    // ==== 게임매니저 호출 ====
    private GameManager _game_Manager;

    private void Awake()
    {
        _game_Manager = GetComponentInParent<GameManager>();
    // =========================
        animationPlayer = FindObjectOfType<AnimationPlayer>();  // 플레이어의 애니메이션 컴퍼넌트
    }


    private void Start()
    {
        Hitpoint();
    }

    // === HP표시 ===
    public void Hitpoint()
    {
        stats.currentHP = stats.maxHP; // 체력 초기화, UI 갱신
        Debug.Log($"현재 체력 {stats.currentHP}"); // 확인용
    }

    // === 레벨업 ===
    public void Levelup()
    {
        stats.level++;

        // === 레벨업 보너스 ===
        stats.maxHP += 10f;                         
        stats.attack += 2f;                          
        stats.defense += 1;
        
        stats.currentHP = stats.maxHP;                 
        Debug.Log($"레벨업! 현재 레벨: {stats.level}"); // ui 추가시 나중에 지우삼
        Hitpoint();
    }
    
    // === 플레이어가 데미지를 받을 시 ===
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - stats.defense); // 데미지 계산

        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            animationPlayer.CharacterDie();
            _game_Manager.GameOver();
        }
        else
        {
            animationPlayer.DamageSuffer();
            Hitpoint(); //확인용
        }    
    }

}
