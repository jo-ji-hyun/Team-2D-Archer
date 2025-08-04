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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StatsUpspeed(); // 실험용
        }
    }

    // === HP표시 ===
    public void Hitpoint()
    {
        _player_UIHelper.UpdateHP(stats.currentHP, stats.maxHP);
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

    // === 최대체력 증가 ===
    public void StatsUpmaxHP()
    {
        stats.maxHP += 10;                 // 최대 HP 증가
        stats.currentHP = stats.maxHP;     // 현재 HP를 최대 HP로 회복
        Hitpoint();
    }
    
    // === 공격력 증가 ===
    public void StatsUpattack()
    {
        stats.attack += 2.0f;                //  공격력 증가
    }

    // === 방어력 증가 ===
    public void StatsUpdefence()
    {
        stats.defense += 1.0f;                //  방어력 증가
    }

    // === 이동속도 증가 ===
    public void StatsUpspeed()
    {
        stats.moveSpeed += 2.0f;                //  이동속도 증가
    }
}
