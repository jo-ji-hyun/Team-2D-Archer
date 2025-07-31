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
            // ü�¹� Image�� fillAmount�� ����Ͽ� ü�� ������ �ð�ȭ
            Hpbar.fillAmount = currentHealth / maxHealth;
        }
    }
}
