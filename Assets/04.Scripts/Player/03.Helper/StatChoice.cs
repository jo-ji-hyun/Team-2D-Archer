using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class StatChoice
{
    public string name;
    public StatType statType;
    public float value;
}

public enum StatType
{
    Attack,
    Defense,
    MoveSpeed,
    AttackSpeed,
    HP
}
