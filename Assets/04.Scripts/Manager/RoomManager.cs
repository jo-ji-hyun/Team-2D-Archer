using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public void OnRoomCleared() // 방이 클리어되었을 때 호출되는 함수.
    {
        // 스킬 랜덤 획득
        SkillManager.Instance.AcquireRandomSkill();
        Debug.Log("방을 클리어하여 새로운 스킬을 얻었습니다!");
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
    }

    void ChexkAllEnemiesDead() // 모든 적이 죽었는지 리스트를 체크해서 클리어가 되었는지 확인하는 함수.
    {
        // 적 매니저의 적이 리스트에 0 즉 모든 적이 죽었을 때 방이 클리어됨.
        if (EnemyManager.Instance.enemies.Count == 0)
        {
            OnRoomCleared();
        }
    }
}
