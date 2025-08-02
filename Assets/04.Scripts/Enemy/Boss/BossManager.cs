using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    private BossController currentBoss;
    private Transform playerTarget;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerTarget(Transform player)
    {
        playerTarget = player;
    }

    public void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogWarning("BossManager: bossPrefab이 설정되어 있지 않습니다.");
            return;
        }

        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        currentBoss = bossObj.GetComponent<BossController>();

        if (currentBoss == null)
        {
            Debug.LogWarning("BossManager: BossController 컴포넌트가 존재하지 않습니다.");
            return;
        }

        if (playerTarget == null)
        {
            Debug.LogWarning("BossManager: playerTarget이 설정되어 있지 않습니다.");
            return;
        }

        currentBoss.Init(this, playerTarget);
    }

    public void UnregisterBoss()
    {
        currentBoss = null;
    }

    public bool IsBossAlive => currentBoss != null;
}
