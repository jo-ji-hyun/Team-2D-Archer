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

        // ��ų ���� ȹ��
        SkillManager.Instance.ShowSkillChoice();
    }

    //void update()
    //{
    //    if (!gamestarted)
    //        return; // ������ ���۵��� �ʾҴٸ� ������Ʈ �Լ� ����.

    //    if (enemymanager.instance.isallspawned && enemymanager.instance.allenemiesdead()) 
    //    {
    //        onroomcleared(); // ��� ���� �׾��� �� �� Ŭ���� �Լ�.
    //    }

    //    if (input.getkeydown(keycode.r))
    //    {
    //        skillmanager.instance.acquirerandomskill();
    //        // �׽�Ʈ�� rŰ ������ ���� ��ų ȹ��
    //    }

    //    if (input.getkeydown(keycode.v))
    //    {
    //        onroomcleared(); // �׽�Ʈ�� vŰ ������ �� Ŭ���� �Լ� ȣ��
    //    }
    //}

    public void StartRoom()
    {
        gameStarted = true; // ���� ���� ���¸� true�� ����.
    }
}
