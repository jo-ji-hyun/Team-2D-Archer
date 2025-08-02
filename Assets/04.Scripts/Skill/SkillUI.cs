using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Skill skillData; // ��ų �̸�
    public Text skillNameText;
    public GameObject playerObj; // �÷��̾� ������Ʈ

    public void Init(Skill skill)
    {
        skillData = skill; // ��ų ������ �ʱ�ȭ
        if (skillNameText != null)
            skillNameText.text = skill.skillName; // UI�� ��ų �̸� ����
    }

    //public void OnClickUseSkill()
    //{
    //    if (playerObj != null)
    //        SkillManager.Instance.UseSkill(skillData, playerObj.transform.position); // �÷��̾� ��ġ���� �߻�.
    //    else
    //        Debug.LogWarning("�÷��̾� ������Ʈ�� ����Ǿ� ���� �ʽ��ϴ�!");
    //}
}
