using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public string skillName; // 스킬 이름

    public void OnClickUseSkill()
    {
        SkillManager.Instance.UseSkill(skillName, transform.position); // 예시 위치
    }
}
