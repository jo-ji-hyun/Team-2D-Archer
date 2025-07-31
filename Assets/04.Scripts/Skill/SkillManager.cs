using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Diagnostics.Contracts;

public class SkillManager : MonoBehaviour
{
    // �̱��� �������� �����ϱ� ���� ����ƽ �ν��Ͻ�
    public static SkillManager Instance;

    // ��� ��ų�� �����ϴ� ����Ʈ
    public List<Skill> allSkills = new List<Skill>();

    // ���� �÷��̾ �������� ��ų ����Ʈ
    public List<Skill> acquiredSkills = new List<Skill>();

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        // �׽�Ʈ �� �⺻ ��ų
        allSkills.Add(new Skill("Flamethrower", "������ ȭ�� ������ ���Ѵ�."));
    }

    // ���ο� ��ų�� �÷��̾�� �ο��ϴ� �Լ�
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // ��ų�� ȹ���� ��ų ��Ͽ� �߰�
        Debug.Log($"��ų ȹ�� : {skill.skillName}"); // ���� ȿ�� ���� ������ ���� �и� ������.
    }

    public void AcquireRandomSkill()
    {
        // ���� ȹ�� ���� ���� ��ų �߿����� �������� ����.
        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));

        if (available.Count > 0)
        {
            int rand = Random.Range(0, available.Count);
            Skill selected = available[rand];
            acquiredSkills.Add(selected);
            Debug.Log($"��ų ȹ��: {selected.skillName} - {selected.description}");
        }
        else
        {
            Debug.Log("ȹ�� ������ ��ų�� �����ϴ�.");
        }
    }

    // �̹� ȹ���� ��ų ����� �����ִ� �Լ�.
    public void ShowAcquiredSkills()
    {
        Debug.Log("==���� ��ų==");
        foreach (var s in acquiredSkills)
        {
            Debug.Log($"{s.skillName} : {s.description}");
        }
    }

    public void UseSkill(Skill skill, Vector3 position)
    {
        Debug.Log($"{skill.skillName} ��ų ���! ��ġ: {position}");
    }
}



