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
                StatType.Attack => "공격력",
                StatType.Defense => "방어력",
                StatType.MoveSpeed => "이동속도",
                StatType.AttackSpeed => "공격속도",
                StatType.HP => "최대 체력",
                _ => "스탯"
            };
            return $"{statName} +{value}";
        }
    }
}
