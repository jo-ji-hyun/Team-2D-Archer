using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerStats Stats = new PlayerStats();

    public int level = 1; // �÷��̾� ����

    void Start()
    {
        Stats.currentHP = Stats.maxHP; // ü�� �ʱ�ȭ, UI ����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // LevelUP(); // ������(�׽�Ʈ��)
        }
    }

    // ������
    public void LevelUP()
    {
        if (Stats.currentHP <= 0)
        {
            return; // �÷��̾ ������ �������� ���ϰ� ����.
        }

        Stats.level++;
        Stats.maxHP += 10; // ������ �� �ִ� HP ����
        Stats.attack += 2f; // ������ �� ���ݷ� ����
        Stats.defense += 1f; // ������ �� ���� ����
        Stats.currentHP = Stats.maxHP; // ������ �� ���� HP�� �ִ� HP�� ȸ��
        Debug.Log($"������! ���� ����: {Stats.level}");
    }
}
