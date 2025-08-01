using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public void OnRoomCleared() // ���� Ŭ����Ǿ��� �� ȣ��Ǵ� �Լ�.
    {
        // ��ų ���� ȹ��
        SkillManager.Instance.AcquireRandomSkill();
        Debug.Log("���� Ŭ�����Ͽ� ���ο� ��ų�� ������ϴ�!");
    }

    void Update()
    {
        if (EnemyManager.Instance.AllEnemiesDead()) // �� �Ŵ����� ��� ���� �׾����� Ȯ���ϴ� �Լ�.
        {
            OnRoomCleared(); // ��� ���� �׾��� �� �� Ŭ���� �Լ�.
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillManager.Instance.AcquireRandomSkill();
            // �׽�Ʈ�� RŰ ������ ���� ��ų ȹ��
        }
    }

    void ChexkAllEnemiesDead() // ��� ���� �׾����� ����Ʈ�� üũ�ؼ� Ŭ��� �Ǿ����� Ȯ���ϴ� �Լ�.
    {
        // �� �Ŵ����� ���� ����Ʈ�� 0 �� ��� ���� �׾��� �� ���� Ŭ�����.
        if (EnemyManager.Instance.enemies.Count == 0)
        {
            OnRoomCleared();
        }
    }
}
