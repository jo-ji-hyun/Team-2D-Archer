using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public Skill skillData; // ��ų �̸�
    public GameObject playerObj; // �÷��̾� ������Ʈ

    public void OnClickUseSkill()
    {
        if (playerObj != null)
            SkillManager.Instance.UseSkill(skillData, transform.position); // ���� ��ġ
        else
            Debug.LogWarning("�÷��̾� ������Ʈ�� ����Ǿ� ���� �ʽ��ϴ�!");
    }
}
