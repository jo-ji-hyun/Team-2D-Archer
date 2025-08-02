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

    //public void onclickuseskill()
    //{
    //    if (playerobj != null)
    //        skillmanager.instance.useskill(skilldata, playerobj.transform.position); // �÷��̾� ��ġ���� �߻�.
    //    else
    //        debug.logwarning("�÷��̾� ������Ʈ�� ����Ǿ� ���� �ʽ��ϴ�!");
    //}
}
