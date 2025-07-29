using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skillmanager : MonoBehaviour
{
    // �̱��� �������� �����ϱ� ���� ����ƽ �ν��Ͻ�
    public static Skillmanager Instance;

    // ��� ��ų�� �����ϴ� ����Ʈ
    public List<Skill> allSkills = new List<Skill>();

    // ���� �÷��̾ �������� ��ų ����Ʈ
    public List<Skill> acquiredSkills = new List<Skill>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // ���ο� ��ų�� �÷��̾�� �ο��ϴ� �Լ�
    public void AcquireSkill(Skill skill)
    {
        acquiredSkills.Add(skill); // ��ų�� ȹ���� ��ų ��Ͽ� �߰�
        Debug.Log($"��ų ȹ�� : {skill.skillName}"); // ���� ȿ�� ���� ������ ���� �и� ������.
    }

    // ���� ȹ������ ���� ��ų �߿��� �������� N�� ����
    public List<Skill> GetRandomSkills(int count)
    {
        List<Skill> candidates = new List<Skill>();

        foreach (Skill skill in allSkills)
        {
            if (!acquiredSkills.Contains(skill))
                candidates.Add(skill);
        }


        // �����ϰ� N�� �̱�
        List<Skill> randomSkills = new List<Skill>();
        for (int i = 0; i < count && candidates.Count > 0; i++)
        {
            int Index = Random.Range(0, candidates.Count);
            randomSkills.Add(candidates[Index]);
            candidates.RemoveAt(Index);
        }

        return randomSkills;
    }
}


