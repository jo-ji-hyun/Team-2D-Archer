using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public Skill skillData; // 스킬 이름
    public GameObject playerObj; // 플레이어 오브젝트

    public void OnClickUseSkill()
    {
        if (playerObj != null)
            SkillManager.Instance.UseSkill(skillData, transform.position); // 예시 위치
        else
            Debug.LogWarning("플레이어 오브젝트가 연결되어 있지 않습니다!");
    }
}
