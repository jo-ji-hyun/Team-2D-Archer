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
    private List<Skill> currentChoices; // ���� ���� ������ ��ų ���

    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        panel.SetActive(false); // �ʱ⿡�� �г��� ����
    }

    public void ShowChoices(List<Skill> skills)
    {
        Debug.Log("ShowChoices ȣ���. ��ư ����: " + buttons.Length + ", ��ų ����: " + skills.Count);

        currentChoices = skills; // ���� ���� ������ ��ų ��� ����
        panel.SetActive(true); // �г��� Ȱ��ȭ
        Time.timeScale = 0f; // ���� �Ͻ� ����

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < skills.Count)
            {
                Debug.Log($"��ư[{i}] Ȱ��ȭ: {skills[i].skillName} ��ư ������Ʈ: {buttons[i].name}");

                buttons[i].gameObject.SetActive(true); // ��ư Ȱ��ȭ
                nameTexts[i].text = skills[i].skillName; // ��ų �̸� ����
                int idx = i;
                buttons[i].onClick.RemoveAllListeners(); // ���� ������ ����
                buttons[i].onClick.AddListener(() => OnClickSelect(idx)); // Ŭ�� ������ �߰�
            }
            else
            {
                Debug.Log($"��ư[{i}] ��Ȱ��ȭ. ��ư ������Ʈ: {buttons[i].name}");
                buttons[i].gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
                nameTexts[i].text = "";
            }
        }
    }

    void OnClickSelect(int idx)
    {
        Debug.Log($"��ų ��ư Ŭ����! idx: {idx}, currentChoices.count: {currentChoices?.Count ?? -1}");

        if (currentChoices == null || idx < 0 || idx >= currentChoices.Count)
        {
            Debug.LogError("OnClickSelect �߸��� �ε��� �Ǵ� currentChoices�� null");
            return;
        }

        SkillManager.Instance.AcquireSkill(currentChoices[idx]); // ������ ��ų ȹ��
        panel.SetActive(false); // �г� ����
        Time.timeScale = 1f; // ���� �簳

        // GameManager.Instance.EndOfWave(); // ���̺� ���� ó�� (�ʿ��)
    }
}