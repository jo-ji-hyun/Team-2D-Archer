using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ
    public TMP_Text levelText; // ���� ���� �ؽ�Ʈ
    public TMP_Text hpText; // ���� HP �ؽ�Ʈ
    public TMP_Text attackText; // ���ݷ� �ؽ�Ʈ
    public TMP_Text defenseText; // ���� �ؽ�Ʈ

    void Update()
    {
        // ���� �ؽ�Ʈ
        levelText.text = $"Lv: {player.Stats.level}";

        hpText.text = $"HP: {player.Stats.currentHP} / {player.Stats.maxHP}";
        attackText.text = $"ATK: {player.Stats.attack}";
        defenseText.text = $"DFS: {player.Stats.defense}";
    }
}
