using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    private StatsManager _stats_Manager;
    public TMP_Text hpText; // 현재 HP 텍스트
    public TMP_Text attackText; // 공격력 텍스트
    public TMP_Text defenseText; // 방어력 텍스트

    void Update()
    {
        hpText.text = $"HP: {_stats_Manager.stats.currentHP} / {_stats_Manager.stats.maxHP}";
        attackText.text = $"Attack: {_stats_Manager.stats.attack}";
        defenseText.text = $"Defense: {_stats_Manager.stats.defense}";
    }
}
