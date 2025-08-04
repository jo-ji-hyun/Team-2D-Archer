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
            return skill != null ? skill.skillName : "스킬 없음";
        }
        else
        {
            return $"{statType} +{value}";
        }
    }
}
