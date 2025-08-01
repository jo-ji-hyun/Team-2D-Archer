using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHelper : MonoBehaviour
{
    public Image Hpbar;

    public TextMeshProUGUI HpText;

    public void UpdateHP(float currentHealth, float maxHealth)
    {
        if (Hpbar != null)
        {
            // === ü�¹� Image�� fillAmount�� ����Ͽ� ü�� ������ �ð�ȭ ===
            if(currentHealth <= 0)
            {
                Hpbar.fillAmount = 0 / maxHealth;
            }
            else
            {
                Hpbar.fillAmount = currentHealth / maxHealth;
            }

            HpText.text = $"{currentHealth.ToString("F0")}"; // F0 �� �Ҽ��� ���� ����
        }
    }
}
