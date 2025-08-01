using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerStats Stats = new PlayerStats();

    public int level = 1; // �÷��̾� ����

    void Start()
    {
        Stats.currentHP = Stats.maxHP; // ü�� �ʱ�ȭ, UI ����
        Debug.Log("HP: " + Stats.currentHP + ", ATK: " + Stats.attack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUP(); 
        } // ������(�׽�Ʈ��)

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f);
        } // ����� �ޱ�(�׽�Ʈ��)

        if (Input.GetKeyDown(KeyCode.J))
        {
            Stats.currentHP = 0;
        } // ��� �����(�׽�Ʈ��)
    }
    // ������ �ޱ�
    public void TakeDamage(float dmg)
    {
        float realDamage = Mathf.Max(0, dmg - Stats.defense);
        Stats.currentHP -= realDamage;
        if (Stats.currentHP < 0) Stats.currentHP = 0; // HP�� 0 ���Ϸ� �������� ���.
        Debug.Log($"������ ����: {realDamage}, ���� ���� HP: {Stats.currentHP}");
    }

    // ������
    public void LevelUP()
    {
        if (Stats.currentHP <= 0)
        {
            Debug.Log("�÷��̾ ����߽��ϴ�."); 
            return; // �÷��̾ ������ �������� ���ϰ� ����.
        }

        Stats.level++;
        Stats.maxHP += 10f; // ������ �� �ִ� HP ����
        Stats.attack += 2f; // ������ �� ���ݷ� ����
        Stats.defense += 1f; // ������ �� ���� ����
        Stats.currentHP = Stats.maxHP; // ������ �� ���� HP�� �ִ� HP�� ȸ��
        Debug.Log($"������! ���� ����: {Stats.level}");

        if (Stats.level == 2)
        {
            Skill fireballSkill = SkillManager.Instance.allSkills.Find(s => s.skillName == "FireBall");
            if (fireballSkill != null && !SkillManager.Instance.acquiredSkills.Contains(fireballSkill))
            {
                SkillManager.Instance.AcquireSkill(fireballSkill);
                Debug.Log("���̾ ��ų�� ȹ���߽��ϴ�");
            }
        }
    }
}
