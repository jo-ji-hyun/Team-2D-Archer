using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// ��ų ������ ��� Ŭ����
[System.Serializable]
public class Skill
{
    public int skillIndex;         // ��ų��ȣ
    public string skillName;       // ��ų �̸�
    public string description;      // ��ų ����
    public float damage;       // �߻�ü ���ط�
    public float speed;        // �߻�ü �ӵ�

    public Skill(int idx, string name, string desc, float dmg, float spd)
    {
        skillIndex = idx;
        skillName = name;   // ��ų �̸� �ʱ�ȭ
        description = desc;  // ��ų ���� �ʱ�ȭ
        damage = dmg;        // ������ �ʱ�ȭ
        speed = spd;         //  �ӵ� �ʱ�ȭ
    }
}

