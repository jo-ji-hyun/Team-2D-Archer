using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[System.Serializable] // �÷��̾� ������ (����)
public class PlayerStats
{
    public int level = 1;
    public int maxHP = 100;
    public int currentHP = 100;
    public float attack = 5.0f;
    public float defense = 0.0f;
    public float moveSpeed = 3.0f;

    // �� �� ���� �߰� �ϰ� ������ �߰�.
}
