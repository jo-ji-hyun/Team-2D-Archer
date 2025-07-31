using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHelper : MonoBehaviour
{
    public Image Hpbar;

    public void UpdateHP(float currentHealth, float maxHealth)
    {
        if (Hpbar != null)
        {
            // 체력바 Image의 fillAmount를 사용하여 체력 비율을 시각화
            Hpbar.fillAmount = currentHealth / maxHealth;
        }
    }
}
