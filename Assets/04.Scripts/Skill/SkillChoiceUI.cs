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
        panel.SetActive(false); // �ʱ⿡�� �г��� ����
    }

    public void ShowChoices(List<Skill> skills)
    {
        Debug.Log("ShowChoices ȣ���. ��ư ����: " + buttons.Length + ", ��ų ����: " + skills.Count);

        currentchoices = skills; // ���� ���� ������ ��ų ��� ����
        panel.SetActive(true); // �г��� Ȱ��ȭ
        Time.timeScale = 0f; // ���� �Ͻ� ����

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < skills.Count)
            {
                Debug.Log($"��ư[{i}] Ȱ��ȭ: {skills[i].skillName} ��ư ������Ʈ: {buttons[i].name}");

                buttons[i].gameObject.SetActive(true); // ��ư Ȱ��ȭ
                nameTexts[i].text = skills[i].skillName; // ��ų �̸� ����
                int idx = i; // ���� ������ �ε��� ����
                buttons[i].onClick.RemoveAllListeners(); // ���� ������ ����
                buttons[i].onClick.AddListener(() => OnClickSelect(idx)); // Ŭ�� ������ �߰�
            }
            else
            {
                Debug.Log($"��ư[{i}] ��Ȱ��ȭ. ��ư ������Ʈ: {buttons[i].name}");
                buttons[i].gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
            }
        }
    }

    void OnClickSelect(int idx)
    {
        Debug.Log($"��ų ��ư Ŭ����! idx: {idx}, currentchoices.Count: {currentchoices?.Count ?? -1}");

        if (currentchoices == null || idx < 0 || idx >= currentchoices.Count)
        {
            Debug.LogError("OnClickSelect �߸��� �ε��� �Ǵ� currentchoices�� null");
            return;
        }

        SkillManager.Instance.AcquireSkill(currentchoices[idx]); // ������ ��ų ȹ��
        panel.SetActive(false); // �г� ����
        Time.timeScale = 1f; // ���� �簳

        //GameManager.Instance.EndOfWave(); // ���� �Ŵ����� ���� ���̺� ���� �˸�
    }
}
