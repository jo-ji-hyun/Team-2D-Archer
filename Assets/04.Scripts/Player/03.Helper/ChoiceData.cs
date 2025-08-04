using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum ChoiceType { Skill, Stat }

[System.Serializable]
public class ChoiceData
{
    public ChoiceType choiceType;

    public string name;
    public StatType statType;
    public float value;

    public Skill skill;

    public string GetDisplayName()
    {
        if (choiceType == ChoiceType.Skill)
        {
            return skill != null ? skill.skillName : "";
        }
        else
        {
            string statName = statType switch
            {
                StatType.Attack => "���ݷ�",
                StatType.Defense => "����",
                StatType.MoveSpeed => "�̵��ӵ�",
                StatType.AttackSpeed => "���ݼӵ�",
                StatType.HP => "�ִ� ü��",
                _ => "����"
            };
            return $"{statName} +{value}";
        }
    }
}
