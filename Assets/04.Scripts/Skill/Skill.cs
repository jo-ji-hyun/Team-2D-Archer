using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// 스킬 정보를 담는 클래스
[System.Serializable]
public class Skill
{
    public int Index;           // 스킬번호
    public string skillName;     // 스킬 이름
    public string description;    // 스킬 설명
    public float damage;           // 발사체 피해량
    public float speed;             // 발사체 속도
    public float coolTime;           // 쿨타임

    public GameObject magicBulletPrefab;

    public float currentCoolTime;     // 현재 쿨타임

    public Skill(int idx, string name, string desc, float dmg, float spd, float cT, GameObject prefab)
    {
        Index = idx;
        skillName = name;   // 스킬 이름 초기화
        description = desc;  // 스킬 설명 초기화
        damage = dmg;        // 데미지 초기화
        speed = spd;         //  속도 초기화
        coolTime = cT;        // 스킬 쿨타임 초기화

        magicBulletPrefab = prefab; // 필드에서 할당
    }
}

