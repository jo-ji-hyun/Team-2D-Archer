using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Diagnostics.Contracts;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    // �̱��� �������� �����ϱ� ���� ����ƽ �ν��Ͻ�
    public static SkillManager Instance;

    // ��� ��ų�� �����ϴ� ����Ʈ
    public List<Skill> allSkills = new List<Skill>();

    // ���� �÷��̾ �������� ��ų ����Ʈ
    public List<Skill> acquiredSkills = new List<Skill>();

    public GameObject fireballPrefab; // ���̾ ������
    public GameObject skillButtonPrefab; // ��ų ��ư ������
    public Transform skillButtonParent; // ��ų ��ư�� ��ġ�� �θ� ������Ʈ

    public float autoFireInterval = 1.0f; // �ڵ� �߻� ���� (�� ����)
    private float autoFireTimer = 0f; // �ڵ� �߻� Ÿ�̸�

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        // �׽�Ʈ �� �⺻ ��ų
        allSkills.Add(new Skill("FireBall", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�."));
    }

    private void Update()
    {
        Skill fireballSkill = acquiredSkills.Find(s => s.skillName == "FireBall");
        if (fireballSkill != null)
        {
            autoFireTimer += Time.deltaTime; // Ÿ�̸� ������Ʈ
            if (autoFireTimer >= autoFireInterval)
            {
                autoFireTimer = 0f; // Ÿ�̸� �ʱ�ȭ

                GameObject playerObj = GameObject.FindWithTag("Player");
                if (playerObj != null)
                    UseSkill(fireballSkill, playerObj.transform.position);
            }
        }
    }

    // ���ο� ��ų�� �÷��̾�� �ο��ϴ� �Լ�
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // ��ų�� ȹ���� ��ų ��Ͽ� �߰�
        Debug.Log($"��ų ȹ�� : {skill.skillName}"); // ���� ȿ�� ���� ������ ���� �и� ������.

        CreateSkillButten(skill); // UI�� ��ų ��ư ����
    }

    public void ShowSkillChoice()
    {
        if (SkillChoiceUI.Instance == null)
        {
            Debug.LogError("SkillChoiceUI �ν��Ͻ��� �����ϴ�. SkillChoiceUI�� ���� �߰��ϼ���.");
            return;
        }

        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));
        List<Skill> choices = new List<Skill>();

        for (int i = 0; i < 3 && available.Count > 0; i++)
        {
            int idx = Random.Range(0, available.Count);
            choices.Add(available[idx]);
            available.RemoveAt(idx); // ������ ��ų�� ��Ͽ��� ����
        }

        SkillChoiceUI.Instance.ShowChoices(choices);
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
    public void CreateSkillButten(Skill skill) // ��ų ��ư�� �����ϴ� �Լ�.
    {
        Debug.Log("[Skill]��ų ��ư ����: " + skill.skillName);

        GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
        Debug.Log("[Skill] ��ư ���� ������: " + btnObj.name);

        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI == null)
            Debug.LogWarning("SkillUI ��ũ��Ʈ�� �����տ� ����");

        else
            Debug.Log("SkillUI ���� �Ϸ�");

        skillUI.Init(skill); // ��ų UI �ʱ�ȭ.
        skillUI.playerObj = GameObject.FindWithTag("Player"); // �÷��̾� ������Ʈ�� ã�Ƽ� �Ҵ�.


        // ���� init �Լ� ���� ���� �Ҵ� �ҷ���:
        // skillUI.skillData = skill;
        // skillUI.skillNameText.text = skill.skillName; // ��ų �̸� ����.
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
            GameObject proj = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
            proj.GetComponent<Projectile>().SetDirection(dir); // �߻�ü ���� ����

            float FireballDamage = Player.Instance.Stats.attack * 0.5f;
        }

        if (skill == null)
        {
            Debug.LogWarning("����� ��ų�� null�Դϴ�.");
            return;
        }
    }
}