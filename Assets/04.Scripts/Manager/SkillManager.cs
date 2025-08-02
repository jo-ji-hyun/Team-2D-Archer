using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // �̱��� ����
    public static SkillManager Instance;

    // ��� ��ų ���
    public List<Skill> allSkills = new List<Skill>();

    // �÷��̾ ������ ��ų ���
    public List<Skill> acquiredSkills = new List<Skill>();

    public GameObject fireballPrefab; // ���̾ ������
    public GameObject iceSpikePrefab;  // ���̽� ���Ǿ� ������
    public GameObject lightningBolt;   // ����Ʈ�� ������

    public GameObject skillButtonPrefab; // ��ų ��ư ������ (UI)
    public Transform  skillButtonParent; // ��ų ��ư �θ� (UI)

    private void Awake()
    {
        Instance = this;

        // ��� ��ų ���
        allSkills.Add(new Skill(0, "FireBall", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�.", 10f, 5f, fireballPrefab));
        allSkills.Add(new Skill(1, "IceSpike", "���� â�� �߻��Ͽ� ���� �󸰴�.", 8f, 7f, iceSpikePrefab));
        allSkills.Add(new Skill(2, "LightningBolt", "������ ��ȯ�Ͽ� ������ ���ظ� �ش�.", 12f, 10f, lightningBolt));

        acquiredSkills.Add(new Skill(0, "FireBall", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�.", 10f, 5f, fireballPrefab)); // Ȯ�ο��߰�
    }

    // ���ο� ��ų ȹ��
    public void AcquireSkill(Skill skill)
    {
        if (!acquiredSkills.Contains(skill))
        {
            acquiredSkills.Add(skill);
            Debug.Log($"��ų ȹ�� : {skill.skillName}");
            CreateSkillButton(skill);
        }
    }

    // ��ų ���� UI�� ������ ��ų ���� 3�� ����
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
            available.RemoveAt(idx);
        }

        SkillChoiceUI.Instance.ShowChoices(choices);
    }

    // ���� ��ų ���� ȹ��
    public void AcquireRandomSkill()
    {
        List<Skill> available = allSkills.FindAll(s => !acquiredSkills.Contains(s));
        if (available.Count > 0)
        {
            int rand = Random.Range(0, available.Count);
            Skill selected = available[rand];
            acquiredSkills.Add(selected);
            Debug.Log($"��ų ȹ��: {selected.skillName} - {selected.description}");
            CreateSkillButton(selected);
        }
        else
        {
            Debug.Log("ȹ�� ������ ��ų�� �����ϴ�.");
        }
    }

    // ��ų UI ��ư ���� �Լ�(Ȯ���)
    public void CreateSkillButton(Skill skill)
    {
        if (skillButtonPrefab == null || skillButtonParent == null) return;

        Debug.Log("[Skill]��ų ��ư ����: " + skill.skillName);
        GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI != null)
        {
            skillUI.Init(skill);
            skillUI.playerObj = GameObject.FindWithTag("Player");
        }
        else
        {
            Debug.LogWarning("SkillUI ��ũ��Ʈ�� �����տ� ����");
        }
    }

    // === ���� ��ų �߻� ===
    public void UseSkill(Vector2 startPosition, Vector2 direction, int skillnum)
    {
        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            if (skillnum == acquiredSkills[i].Index)
            {
                GameObject magicPrefab = acquiredSkills[i].magicBulletPrefab;
                GameObject proj = Instantiate(magicPrefab, startPosition, Quaternion.identity);
                Debug.Log("��ų �ߵ�");
            }

        }
    }

   
}