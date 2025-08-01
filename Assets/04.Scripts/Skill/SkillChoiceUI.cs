using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoiceUI : MonoBehaviour
{
    public static SkillChoiceUI Instance;

    public GameObject panel; // ��ų ���� �г�
    public Button[] buttons; // ��ų ���� ��ư��
    public Text[] nameTexts; // ��ų �̸� �ؽ�Ʈ
    private List<Skill> currentchoices; // ���� ���� ������ ��ų ���

    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        Debug.Log("SkillChoiceUI Awake�� ȣ���");
        panel.SetActive(false); // �ʱ⿡�� �г��� ����
    }

    public void ShowChoices(List<Skill> skills)
    {
        currentchoices = skills; // ���� ���� ������ ��ų ��� ����
        panel.SetActive(true); // �г��� Ȱ��ȭ
        Time.timeScale = 0f; // ���� �Ͻ� ����

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < skills.Count)
            {
                buttons[i].gameObject.SetActive(true); // ��ư Ȱ��ȭ
                nameTexts[i].text = skills[i].skillName; // ��ų �̸� ����
                int idx = i; // ���� ������ �ε��� ����
                buttons[i].onClick.RemoveAllListeners(); // ���� ������ ����
                buttons[i].onClick.AddListener(() => OnClickSelect(idx)); // Ŭ�� ������ �߰�
            }
            else
            {
                buttons[i].gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
            }
        }
    }

    void OnClickSelect(int idx)
    {
        SkillManager.Instance.AcquireSkill(currentchoices[idx]); // ������ ��ų ȹ��
        panel.SetActive(false); // �г� ����
        Time.timeScale = 1f; // ���� �簳
    }
}
