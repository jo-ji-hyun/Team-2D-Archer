using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHp : MonoBehaviour
{
    public Image BossHpbar;

    public TextMeshProUGUI HpText;

    public void UpdateHP(float currentHealth, float maxHealth)
    {
        if (BossHpbar != null)
        {
            // === ü�¹� Image�� fillAmount�� ����Ͽ� ü�� ������ �ð�ȭ ===
            if (currentHealth <= 0)
            {
                BossHpbar.fillAmount = 0 / maxHealth;
            }
            else
            {
                BossHpbar.fillAmount = currentHealth / maxHealth;
            }

            HpText.text = $"{currentHealth.ToString("F0")}"; // F0 �� �Ҽ��� ���� ����
        }
    }
}
