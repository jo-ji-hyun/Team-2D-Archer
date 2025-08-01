using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// 스킬 정보를 담는 클래스
[System.Serializable]
public class Skill
{
    public string skillName; // 스킬 이름
    public string description; // 스킬 설명

    public Skill(string name, string desc)
    {
        skillName = name; // 스킬 이름 초기화
        description = desc; // 스킬 설명 초기화
    }
}

