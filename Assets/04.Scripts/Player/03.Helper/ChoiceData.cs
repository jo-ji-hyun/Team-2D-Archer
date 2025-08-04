using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        return choiceType == ChoiceType.Skill ? skill.skillName : name;
    }
}
