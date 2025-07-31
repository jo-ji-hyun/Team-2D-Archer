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
        Hitpoint();
    }

    // === HPǥ�� ===
    public void Hitpoint()
    {
        Debug.Log($"���� ü�� {stats.currentHP}"); // Ȯ�ο�
    }

    // === ������ ===
    public void Levelup()
    {
        stats.level++;

        // === ������ ���ʽ� ===
        stats.maxHP += 10f;                         
        stats.attack += 1f;                          
        stats.defense += 0.5f;
        
        stats.currentHP = stats.maxHP;                 
        Debug.Log($"������! ���� ����: {stats.level}"); // ui �߰��� ���߿� �����
        Hitpoint();
    }
    
    // === �÷��̾ �������� ���� �� ===
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - stats.defense); // ������ ���
        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            Debug.LogError("�÷��̾� ���");
            _game_Manager.GameOver();
        }
        else
        {
            Hitpoint(); //Ȯ�ο�
        }    
    }

}
