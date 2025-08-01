using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public Player player; // Player 스크립트
    public TMP_Text levelText; // 현재 레벨 텍스트
    public TMP_Text hpText; // 현재 HP 텍스트
    public TMP_Text attackText; // 공격력 텍스트
    public TMP_Text defenseText; // 방어력 텍스트

    void Update()
    {
        // 레벨 텍스트
        levelText.text = $"Lv: {player.Stats.level}";

        hpText.text = $"HP: {player.Stats.currentHP} / {player.Stats.maxHP}";
        attackText.text = $"ATK: {player.Stats.attack}";
        defenseText.text = $"DFS: {player.Stats.defense}";
    }
}
