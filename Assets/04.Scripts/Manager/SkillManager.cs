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

    // === ��ų������ �޾ƿ� ===
    public Skill skillData;

    public float _auto_Fire_Interval = 1.0f; // �ڵ� �߻� ���� (�� ����)
    private float _auto_Fire_Timer = 0f; // �ڵ� �߻� Ÿ�̸�

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        // ��ų�� �߰���
        allSkills.Add(new Skill("FireBall", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�.", 10f, 5f));
    }

    private void Update()
    {
        Skill fireballSkill = acquiredSkills.Find(s => s.skillName == "FireBall");
        //if (fireballSkill != null)
        //{
        //    autoFireTimer += Time.deltaTime; // Ÿ�̸� ������Ʈ
        //    if (autoFireTimer >= autoFireInterval)
        //    {
        //        autoFireTimer = 0f; // Ÿ�̸� �ʱ�ȭ

        //        GameObject playerObj = GameObject.FindWithTag("Player");
        //        if (playerObj != null)
        //           // UseSkill(fireballSkill, playerObj.transform.position);
        //    }
        //}
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
        }
        else
        {
            Debug.Log("ȹ�� ������ ��ų�� �����ϴ�.");
        }
    }

    public void UseSkill(string skill)//, Vector3 position)
    {
        Debug.Log($"{acquiredSkills} ��ų ���!");

        //// �÷��̾� ��ġ���� �߻��ϵ��� ����
        //GameObject playerObj = GameObject.FindWithTag("Player");
        //Vector3 spawnPos = playerObj != null ? playerObj.transform.position : position;

        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            Skill skillData = acquiredSkills[i];
            Debug.Log("��ų ��ȣ " + i + " �̸�: " + skillData.skillName);

            float FireballDamage = Player.Instance.Stats.attack * 0.5f;
        }

        if (skill == null)
        {
            Debug.LogWarning("����� ��ų�� null�Դϴ�.");
            return;
        }
    }
}



