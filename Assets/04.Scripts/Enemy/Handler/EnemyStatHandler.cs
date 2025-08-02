using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [Header("스탯 설정")]
    [Range(1f, 100f)]
    [SerializeField] private float MaxHP = 10f;
    public float Health => MaxHP; // 읽기 전용 체력

    [Range(1f, 20f)]
    [SerializeField] private float speed = 3f;

    public float Speed => speed; // 읽기 전용 속도

    private float currentHp;

    private void Awake()
    {
        currentHp = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"[EnemyStatHandler] 피해를 입음: {damage}, 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            GetComponent<EnemyBaseController>()?.Death();
        }
    }
}