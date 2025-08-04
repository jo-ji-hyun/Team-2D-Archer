using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatChoiceUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] nameTexts;
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private PlayerController playerController;

    private List<ChoiceData> currentChoices;

    public void ShowChoices(List<ChoiceData> choices)
    {
        if (choices.Count != 3)
        {
            return;
        }

        currentChoices = choices;
        panel.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].gameObject.SetActive(true);
            nameTexts[i].text = choices[i].GetDisplayName();
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnChoiceSelected(index));
            buttons[i].interactable = true;
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
                    statsManager.StatsUpattack();
                    weaponHandler.Power += choice.value;
                    break;

                case StatType.Defense:
                    statsManager.StatsUpdefence();
                    break;

                case StatType.MoveSpeed:
                    statsManager.StatsUpspeed();
                    break;

                case StatType.AttackSpeed:
                    weaponHandler.DecreaseAttackDelay(choice.value);
                    break;

                case StatType.HP:
                    statsManager.StatsUpmaxHP();
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

    public void SetUp(StatsManager statsManager, WeaponHandler weapon)
    {
        this.statsManager = statsManager;
        this.weaponHandler = weapon;
    }
}

