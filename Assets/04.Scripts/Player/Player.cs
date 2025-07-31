using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // === StatsManager ���� ===
    private StatsManager _stats_Manager;

    private void Awake()
    {
        _stats_Manager = FindObjectOfType<StatsManager>();
    }
    // ===========================

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUP(); 
        } // ������(�׽�Ʈ��)

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f);
        } // ����� �ޱ�(�׽�Ʈ��)

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(99999f);
        } // ��� �����(�׽�Ʈ��)
    }

    // === ������ �ޱ� ===
    public void TakeDamage(float dmg)
    {
        _stats_Manager.TakeDamage(dmg);
    }

    // === ������ ===
    public void LevelUP()
    {
        _stats_Manager.Levelup();
    }
}
