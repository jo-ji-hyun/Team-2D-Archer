using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isCleared = false; // �� Ŭ���� ���¸� ��Ÿ���� ����.
    public bool gameStarted = false; // ���� ���� ���θ� ��Ÿ���� ����.
    public void OnRoomCleared() // ���� Ŭ����Ǿ��� �� ȣ��Ǵ� �Լ�.
    {
        if (isCleared) return; // �̹� ���� Ŭ����Ǿ����� �Լ� ����.
        isCleared = true; // �� Ŭ���� ���¸� true�� ����.

        Debug.Log("<color=yellow>====================</color>");
        Debug.Log("<color=lime>�� Ŭ����! (OnRoomCleared ȣ���)</color>");
        Debug.Log("<color=yellow>====================</color>");
        // ��ų ���� ȹ��
        SkillManager.Instance.ShowSkillChoice();
        Debug.Log("���� Ŭ�����Ͽ� ���ο� ��ų�� �����ϼ���!");
    }

    void Update()
    {
        Debug.Log($"[RoomManager] gameStarted: {gameStarted}, IsAllSpawned: {EnemyManager.Instance.IsAllSpawned}, AllEnemiesDead: {EnemyManager.Instance.AllEnemiesDead()}");

        if (!gameStarted)
            return; // ������ ���۵��� �ʾҴٸ� ������Ʈ �Լ� ����.

        if (EnemyManager.Instance.IsAllSpawned && EnemyManager.Instance.AllEnemiesDead()) 
        {
            Debug.Log("[RoomManager] ���� ����: OnRoomCleared() ȣ��");
            OnRoomCleared(); // ��� ���� �׾��� �� �� Ŭ���� �Լ�.
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillManager.Instance.AcquireRandomSkill();
            // �׽�Ʈ�� RŰ ������ ���� ��ų ȹ��
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OnRoomCleared(); // �׽�Ʈ�� VŰ ������ �� Ŭ���� �Լ� ȣ��
        }
    }

    public void StartRoom()
    {
        gameStarted = true; // ���� ���� ���¸� true�� ����.
        Debug.Log("RoomManager.StartRoom() ȣ���! gameStarted true");
    }
}
