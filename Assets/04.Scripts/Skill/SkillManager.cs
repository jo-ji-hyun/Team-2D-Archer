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

    public GameObject fireballPrefab; // ���̾ ������
    public GameObject skillButtenPrefeb; // ��ų ��ư ������
    public Transform skillButtenParent; // ��ų ��ư�� ��ġ�� �θ� ������Ʈ

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        // �׽�Ʈ �� �⺻ ��ų
        allSkills.Add(new Skill("Flamethrower", "������ ȭ�� ������ ���Ѵ�."));
        allSkills.Add(new Skill("FireBall", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�."));
    }

    // ���ο� ��ų�� �÷��̾�� �ο��ϴ� �Լ�
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // ��ų�� ȹ���� ��ų ��Ͽ� �߰�
        Debug.Log($"��ų ȹ�� : {skill.skillName}"); // ���� ȿ�� ���� ������ ���� �и� ������.

        CreateSkillButten(skill); // UI�� ��ų ��ư ����
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
            CreateSkillButten(selected);
        }
        else
        {
            Debug.Log("ȹ�� ������ ��ų�� �����ϴ�.");
        }
    }
    public void CreateSkillButten(Skill skill)
    {
        Debug.Log("[Skill]��ų ��ư ����: " + skill.skillName);

        GameObject btnObj = Instantiate(skillButtenPrefeb, skillButtenParent);
        Debug.Log("[Skill] ��ư ���� ������: " + btnObj.name);

        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI == null)
            Debug.LogWarning("SkillUI ��ũ��Ʈ�� �����տ� ����");

        else
            Debug.Log("SkillUI ���� �Ϸ�");

        skillUI.Init(skill); // ��ų UI �ʱ�ȭ
        skillUI.playerObj = GameObject.FindWithTag("Player"); // �÷��̾� ������Ʈ�� ã�Ƽ� �Ҵ�


        // ���� init �Լ� ���� ���� �Ҵ� �ҷ���:
        // skillUI.skillData = skill;
        // skillUI.skillNameText.text = skill.skillName; // ��ų �̸� ����
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

        // �÷��̾� ��ġ���� �߻��ϵ��� ����
        GameObject playerObj = GameObject.FindWithTag("Player");
        Vector3 spawnPos = playerObj != null ? playerObj.transform.position : position;

        if (skill.skillName == "FireBall")
        {
            Vector2 dir = Vector2.right; // ���÷� ������ �������� �߻�

            GameObject proj = Instantiate(fireballPrefab, position, Quaternion.identity);

            proj.GetComponent<Projectile>().SetDirection(dir); // �߻�ü ���� ����
        }
    }


}



