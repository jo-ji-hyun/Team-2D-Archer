using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// ��ų ������ ��� Ŭ����
[System.Serializable]
public class Skill
{
    public string skillName; // ��ų �̸�
    public string description; // ��ų ����

    public Skill(string name, string desc)
    {
        skillName = name; // ��ų �̸� �ʱ�ȭ
        description = desc; // ��ų ���� �ʱ�ȭ
    }
}

