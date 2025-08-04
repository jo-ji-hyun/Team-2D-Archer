using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] nameTexts;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private PlayerController playerController;

    private List<ChoiceData> currentChoices;

    private void Awake()
    {
        playerStats = playerController.GetStats();
    }

    public void ShowChoices(List<ChoiceData> choices)
    {
        currentChoices = choices;
        panel.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < choices.Count)
            {
                int index = i;
                buttons[i].gameObject.SetActive(true);
                nameTexts[i].text = choices[i].GetDisplayName();
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => OnChoiceSelected(index));
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
                nameTexts[i].text = "";
            }
            
        }
    }

    private void OnChoiceSelected(int index)
    {
        var choice = currentChoices[index];

        if (choice.choiceType == ChoiceType.Stat)
        {
            switch (choice.statType)
            {
                case StatType.Attack:
                    playerStats.attack += choice.value;
                    weaponHandler.Power += choice.value;
                    break;

                case StatType.Defense:
                    playerStats.defense += choice.value;
                    break;

                case StatType.MoveSpeed:
                    playerStats.moveSpeed += choice.value;
                    break;

                case StatType.AttackSpeed:
                    weaponHandler.Delay -= Mathf.Max(0, 05f, weaponHandler.Delay - choice.value);
                    break;

                case StatType.HP:
                    playerStats.maxHP += (int)choice.value;
                    playerStats.currentHP = playerStats.maxHP;
                    break;
            }
        }
        else if (choice.choiceType == ChoiceType.Skill)
        {
            SkillManager.Instance.AcquireSkill(choice.skill);
        }

        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetUp(PlayerStats stats, WeaponHandler weapon)
    {
        this.playerStats = stats;
        this.weaponHandler = weapon;
    }
}

