using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillExecutor : MonoBehaviour
{
    public GameObject skillPrefab; // 스킬 프리팹

    public void Use(Vector3 position) // 외부에는 이 메서드를 호출하면 해당 위치에 이펙트를 생성.
    {
        Instantiate(skillPrefab, position, Quaternion.identity); // 이펙트 프리팹을 지정된 위치에 생성.
    }
       
}
