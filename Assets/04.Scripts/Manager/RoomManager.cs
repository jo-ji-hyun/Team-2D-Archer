using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public void OnRoomCleared() // ���� Ŭ����Ǿ��� �� ȣ��Ǵ� �Լ�.
    {
        // ��ų ���� ȹ��
        SkillManager.Instance.ShowSkillChoice();
    }

    //void Update()
    //{
    //    if (!gameStarted)
    //        return; // ������ ���۵��� �ʾҴٸ� ������Ʈ �Լ� ����.

    //    if (EnemyManager.Instance.IsAllSpawned && EnemyManager.Instance.AllEnemiesDead()) 
    //    {
    //        OnRoomCleared(); // ��� ���� �׾��� �� �� Ŭ���� �Լ�.
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        SkillManager.Instance.AcquireRandomSkill();
    //        // �׽�Ʈ�� RŰ ������ ���� ��ų ȹ��
    //    }

    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        OnRoomCleared(); // �׽�Ʈ�� VŰ ������ �� Ŭ���� �Լ� ȣ��
    //    }
    //}
}
