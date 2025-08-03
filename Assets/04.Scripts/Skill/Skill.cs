using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// ��ų ������ ��� Ŭ����
[System.Serializable]
public class Skill
{
    public int Index;           // ��ų��ȣ
    public string skillName;     // ��ų �̸�
    public string description;    // ��ų ����
    public float damage;           // �߻�ü ���ط�
    public float speed;             // �߻�ü �ӵ�
    public float coolTime;           // ��Ÿ��

    public GameObject magicBulletPrefab;

    public float currentCoolTime;     // ���� ��Ÿ��

    public Skill(int idx, string name, string desc, float dmg, float spd, float cT, GameObject prefab)
    {
        Index = idx;
        skillName = name;   // ��ų �̸� �ʱ�ȭ
        description = desc;  // ��ų ���� �ʱ�ȭ
        damage = dmg;        // ������ �ʱ�ȭ
        speed = spd;         //  �ӵ� �ʱ�ȭ
        coolTime = cT;        // ��ų ��Ÿ�� �ʱ�ȭ

        magicBulletPrefab = prefab; // �ʵ忡�� �Ҵ�
    }
}

