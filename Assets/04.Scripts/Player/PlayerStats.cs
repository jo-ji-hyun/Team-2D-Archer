using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable] // 플레이어 데이터 (상자)
public class PlayerStats
{
    public int level = 1;
    public float maxHP = 100.0f;
    public float currentHP = 100.0f;
    public float attack = 5.0f;
    public float defense = 0.0f;
    public float moveSpeed = 3.0f;
    public float attackSpeed = 1.0f;

    // 그 외 스탯 추가 하고 싶으면 추가.
}
