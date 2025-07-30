using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int level = 1;
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float attack = 10f;
    public float defense = 5f;
    public float moveSpeed = 5f;

    // 그 외 스탯 추가 하고 싶으면 추가.
}
