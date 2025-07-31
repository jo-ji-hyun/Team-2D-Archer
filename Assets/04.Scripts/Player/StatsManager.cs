using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // === �÷��̾� ���ݿ��� ������ ������ ===
    public PlayerStats stats = new PlayerStats();

    // ==== ���ӸŴ��� ȣ�� ====
    private GameManager _game_Manager;

    private void Awake()
    {
        _game_Manager = GetComponentInParent<GameManager>();
    }
    // =========================

    private void Start()
    {
        stats.currentHP = stats.maxHP; // ü�� �ʱ�ȭ, UI ����
    }

    public void Levelup()
    {
        if (stats.currentHP <= 0) // Ȥ�� �𸣴�
        {
            _game_Manager.GameOver(); // ���ӿ��� ȣ��
        }

        stats.level++;

        // === ������ ���ʽ� ===
        stats.maxHP += 10f;                         
        stats.attack += 2f;                          
        stats.defense += 1;
        
        stats.currentHP = stats.maxHP;                 
        Debug.Log($"������! ���� ����: {stats.level}"); // ���߿� �����
    }
    
    // === �÷��̾ �������� ���� �� ===
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - stats.defense); // ������ ���

        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            _game_Manager.GameOver();
        }

        Debug.Log($"������ ����: {realDamage}, ���� ���� HP: {stats.currentHP}"); // ���߿� ����
    }

}
