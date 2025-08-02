using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isCleared = false; // 방 클리어 상태를 나타내는 변수.
    public bool gameStarted = false; // 게임 시작 여부를 나타내는 변수.
    public void OnRoomCleared() // 방이 클리어되었을 때 호출되는 함수.
    {
        if (isCleared) return; // 이미 방이 클리어되었으면 함수 종료.
        isCleared = true; // 방 클리어 상태를 true로 설정.

        // 스킬 랜덤 획득
        SkillManager.Instance.ShowSkillChoice();
    }

    //void Update()
    //{
    //    if (!gameStarted)
    //        return; // 게임이 시작되지 않았다면 업데이트 함수 종료.

    //    if (EnemyManager.Instance.IsAllSpawned && EnemyManager.Instance.AllEnemiesDead()) 
    //    {
    //        OnRoomCleared(); // 모든 적이 죽었을 떄 방 클리어 함수.
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        SkillManager.Instance.AcquireRandomSkill();
    //        // 테스트용 R키 누르면 랜덤 스킬 획득
    //    }

    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        OnRoomCleared(); // 테스트용 V키 누르면 방 클리어 함수 호출
    //    }
    //}

    public void StartRoom()
    {
        gameStarted = true; // 게임 시작 상태를 true로 설정.
    }
}
