using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // === �÷��̾� ���ݿ��� ������ ������ ===
    public PlayerStats stats = new PlayerStats();

    // === HP�� ȣ�� ===
    private PlayerUIHelper _player_UIHelper;

    // ==== ���ӸŴ��� ȣ�� ====
    private GameManager _game_Manager;

    private void Awake()
    {
        _game_Manager = GetComponentInParent<GameManager>();
        // =========================

        // === UI���۸� ���Ӱ� ����� ===
        _player_UIHelper = FindAnyObjectByType<PlayerUIHelper>();
    }

    private void Start()
    {
        stats.currentHP = stats.maxHP; // ü�� �ʱ�ȭ, UI ����
        Hitpoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StatsUpspeed(); // �����
        }
    }

    // === HPǥ�� ===
    public void Hitpoint()
    {
        _player_UIHelper.UpdateHP(stats.currentHP, stats.maxHP);
    }
    
    // === �÷��̾ �������� ���� �� ===
    public void TakeDamage(int dmg)
    {
        int realDamage = (int)Mathf.Max(0, dmg - stats.defense); // ������ ���
        stats.currentHP -= realDamage;

        if (stats.currentHP <= 0)
        { 
            stats.currentHP = 0;
            Hitpoint();
            Debug.LogError("�÷��̾� ���");
            _game_Manager.GameOver();
        }
        else
        {
            Hitpoint(); 
        }    
    }

    // === �ִ�ü�� ���� ===
    public void StatsUpmaxHP()
    {
        stats.maxHP += 10;                 // �ִ� HP ����
        stats.currentHP = stats.maxHP;     // ���� HP�� �ִ� HP�� ȸ��
        Hitpoint();
    }
    
    // === ���ݷ� ���� ===
    public void StatsUpattack()
    {
        stats.attack += 2.0f;                //  ���ݷ� ����
    }

    // === ���� ���� ===
    public void StatsUpdefence()
    {
        stats.defense += 1.0f;                //  ���� ����
    }

    // === �̵��ӵ� ���� ===
    public void StatsUpspeed()
    {
        stats.moveSpeed += 2.0f;                //  �̵��ӵ� ����
    }
}
