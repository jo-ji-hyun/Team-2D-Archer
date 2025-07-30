using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public string skillName; // ��ų �̸�

    public void OnClickUseSkill()
    {
        SkillManager.Instance.UseSkill(skillName, transform.position); // ���� ��ġ
    }
}
