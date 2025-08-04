using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [Header("보스 설정")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    public BossController CurrentBoss { get; private set; }
    private Transform playerTarget;

    public GameObject bossHpObject;


    private void Awake()
    {

        bossHpObject.SetActive(false);

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
        CurrentBoss = bossObj.GetComponent<BossController>();
        bossHpObject.SetActive(true);

        if (CurrentBoss == null)
        {
            Debug.LogWarning("BossManager: BossController 컴포넌트가 존재하지 않습니다.");
            return;
        }

        if (playerTarget == null)
        {
            Debug.LogWarning("BossManager: playerTarget이 설정되어 있지 않습니다.");
            return;
        }

        CurrentBoss.Init(this, playerTarget);
        // EnemyManager에서 activeEnemies 등록
        EnemyManager.Instance.activeEnemies.Add(CurrentBoss);
    }

    public void UnregisterBoss()
    {
        CurrentBoss = null;
    }

}
