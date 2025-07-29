using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// ��ų ������ ��� Ŭ����
[System.Serializable]
public class Skill
{
    public string skillName; // ��ų �̸�
    public string description; // ��ų ����(UI�� ǥ�õ�)
    public Sprite icon; // ��ų ������
    public SkillType type; // ��ų Ÿ�� (����, ���, �ӵ�, ġ�� ��)
    public float value; // ��ų�� ȿ�� ��ġ
}

// ��ų�� ������ �����ϱ� ����.
public enum SkillType
{
    Attack,
    Defense,
    Speed,
    Heal,
    Special
}