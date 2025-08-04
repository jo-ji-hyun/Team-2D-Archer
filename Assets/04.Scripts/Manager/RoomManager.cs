using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public void OnRoomCleared() // 방이 클리어되었을 때 호출되는 함수.
    {
        // 스킬 랜덤 획득
        SkillManager.Instance.ShowSkillChoice();
    }

    //void update()
    //{
    //    if (!gamestarted)
    //        return; // 게임이 시작되지 않았다면 업데이트 함수 종료.

    //    if (enemymanager.instance.isallspawned && enemymanager.instance.allenemiesdead()) 
    //    {
    //        onroomcleared(); // 모든 적이 죽었을 떄 방 클리어 함수.
    //    }

    //    if (input.getkeydown(keycode.r))
    //    {
    //        skillmanager.instance.acquirerandomskill();
    //        // 테스트용 r키 누르면 랜덤 스킬 획득
    //    }

    //    if (input.getkeydown(keycode.v))
    //    {
    //        onroomcleared(); // 테스트용 v키 누르면 방 클리어 함수 호출
    //    }
    //}
}
