using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// 스킬 정보를 담는 클래스
[System.Serializable]
public class Skill
{
    public string skillName; // 스킬 이름
    public string description; // 스킬 설명(UI에 표시됨)
    public Sprite icon; // 스킬 아이콘
    public SkillType type; // 스킬 타입 (공격, 방어, 속도, 치유 등)
    public float value; // 스킬의 효과 수치
}

// 스킬의 종류를 구분하기 위한.
public enum SkillType
{
    Attack,
    Defense,
    Speed,
    Heal,
    Special
}