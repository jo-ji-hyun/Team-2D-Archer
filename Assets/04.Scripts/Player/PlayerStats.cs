using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable] // �÷��̾� ������ (����)
public class PlayerStats
{
    public int level = 1;
    public float maxHP = 100.0f;
    public float currentHP = 100.0f;
    public float attack = 5.0f;
    public float defense = 0.0f;
    public float moveSpeed = 3.0f;
    public float attackSpeed = 1.0f;

    // �� �� ���� �߰� �ϰ� ������ �߰�.
}
