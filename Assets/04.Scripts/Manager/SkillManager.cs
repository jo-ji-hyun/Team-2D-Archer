using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // �̱��� ����
    public static SkillManager Instance;

    // === ��ų Ȯ�� ===
    public bool _isSkill;
    public float AbilityPower; // ��ų ������
    public float AbilitySpeed;  // ��ų �ӵ�

    // === �ٸ� �Ŵ��� ���� ===
    private StatsManager _stats_Manager;

    private ShootManager _shoot_Manager;


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
        allSkills.Add(new Skill(0, "���̾� ��", "ȭ������ �߻��Ͽ� ������ ���ظ� �ش�.", 10f, 5f, 2.0f, fireballPrefab));
        allSkills.Add(new Skill(1, "��ũ ���Ǿ�", "��� â�� �߻��Ͽ� ���� �󸰴�.", 12f, 7f, 4.0f, iceSpikePrefab));
        allSkills.Add(new Skill(2, "����Ʈ��", "������ ��ȯ�Ͽ� ������ ���ظ� �ش�.", 15f, 10f, 6.1f, lightningBolt));

    }
    void Update()
    {
        // === �� �����Ӹ��� ��� ��ų�� ��Ÿ���� ���ҽ�ŵ�ϴ�. ===
        for (int i = 0; i < acquiredSkills.Count; i++)
        {
            if (acquiredSkills[i].currentCoolTime > 0)
            {
                acquiredSkills[i].currentCoolTime -= Time.deltaTime;
            }
        }
    }

    // ���ο� ��ų ȹ��
    public void AcquireSkill(Skill skill)
    {
        if (!acquiredSkills.Contains(skill))
        {
            acquiredSkills.Add(skill);
            CreateSkillButton(skill);
        }
    }

    // ��ų ���� UI�� ������ ��ų ���� 3�� ����
    public void ShowSkillChoice()
    {
        if (SkillChoiceUI.Instance == null)
        {
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

        GameObject btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
        SkillUI skillUI = btnObj.GetComponent<SkillUI>();
        if (skillUI != null)
        {
            skillUI.Init(skill);
            skillUI.playerObj = GameObject.FindWithTag("Player");
        }
        else
        {
            return;
        }
    }

    // === ���� ��ų �߻� ===
    public void UseSkill(RangeWeapon range, Vector2 startPosition, Vector2 direction, int skillnum)
    {
        for (int i = 0; i < skillnum; i++)
        {
            int listIndex = skillnum - 1;

            var skillData = acquiredSkills[listIndex];

            GameObject magicPrefab = skillData.magicBulletPrefab;
            GameObject proj = Instantiate(magicPrefab, startPosition, Quaternion.identity);

            // === ��������� ���� �Ѱ��� ===
            AbilityPower = skillData.damage;
            AbilitySpeed = skillData.speed;

            MagicShoot magicShoot = proj.GetComponent<MagicShoot>();

            magicShoot.Init(direction, range, this._stats_Manager, this._shoot_Manager, this);
        }
    }

    // === ���� �Ŵ����� ������ ===
    public void GiveToSkillManager(StatsManager stats)
    {
        this._stats_Manager = stats;
    }

}