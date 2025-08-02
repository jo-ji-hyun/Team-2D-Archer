using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Skill skillData; // 스킬 이름
    public Text skillNameText;
    public GameObject playerObj; // 플레이어 오브젝트

    public void Init(Skill skill)
    {
        skillData = skill; // 스킬 데이터 초기화
        if (skillNameText != null)
            skillNameText.text = skill.skillName; // UI에 스킬 이름 설정
    }

    //public void OnClickUseSkill()
    //{
    //    if (playerObj != null)
    //        SkillManager.Instance.UseSkill(skillData, playerObj.transform.position); // 플레이어 위치에서 발사.
    //    else
    //        Debug.LogWarning("플레이어 오브젝트가 연결되어 있지 않습니다!");
    //}
}
