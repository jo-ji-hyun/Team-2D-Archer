using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isCleared = false; // 방 클리어 상태를 나타내는 변수.
    public void OnRoomCleared() // 방이 클리어되었을 때 호출되는 함수.
    {
        if (isCleared) return; // 이미 방이 클리어되었으면 함수 종료.
        isCleared = true; // 방 클리어 상태를 true로 설정.

        Debug.Log("<color=yellow>====================</color>");
        Debug.Log("<color=lime>방 클리어! (OnRoomCleared 호출됨)</color>");
        Debug.Log("<color=yellow>====================</color>");
        // 스킬 랜덤 획득
        SkillManager.Instance.ShowSkillChoice();
        Debug.Log("방을 클리어하여 새로운 스킬을 선택하세요!");
    }

    void Update()
    {
        if (EnemyManager.Instance.AllEnemiesDead()) // 적 매니저에 모든 적이 죽었는지 확인하는 함수.
        {
            OnRoomCleared(); // 모든 적이 죽었을 떄 방 클리어 함수.
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillManager.Instance.AcquireRandomSkill();
            // 테스트용 R키 누르면 랜덤 스킬 획득
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OnRoomCleared(); // 테스트용 V키 누르면 방 클리어 함수 호출
        }
    }
}
