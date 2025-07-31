using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // === StatsManager 참고 ===
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
        } // 레벨업(테스트용)

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f);
        } // 대미지 받기(테스트용)

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(99999f);
        } // 즉사 대미지(테스트용)
    }

    // === 데미지 받기 ===
    public void TakeDamage(float dmg)
    {
        _stats_Manager.TakeDamage(dmg);
    }

    // === 레벨업 ===
    public void LevelUP()
    {
        _stats_Manager.Levelup();
    }
}
