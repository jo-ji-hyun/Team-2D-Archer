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
            // === 체력바 Image의 fillAmount를 사용하여 체력 비율을 시각화 ===
            if(currentHealth <= 0)
            {
                Hpbar.fillAmount = 0 / maxHealth;
            }
            else
            {
                Hpbar.fillAmount = currentHealth / maxHealth;
            }

            HpText.text = $"{currentHealth.ToString("F0")}"; // F0 이 소수점 없이 전개
        }
    }
}
