using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [Header("���� ����")]
    [Range(1f, 100f)]
    [SerializeField] private float MaxHP = 10f;
    public float Health => MaxHP; // �б� ���� ü��

    [Range(1f, 20f)]
    [SerializeField] private float speed = 3f;

    public float Speed => speed; // �б� ���� �ӵ�

    private float currentHp;

    private void Awake()
    {
        currentHp = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"[EnemyStatHandler] ���ظ� ����: {damage}, ���� ü��: {currentHp}");

        if (currentHp <= 0)
        {
            GetComponent<EnemyBaseController>()?.Death();
        }
    }
}