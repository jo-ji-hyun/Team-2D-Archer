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

    /* 실험용 강제시작
    private void Start()
    {
        SpawnBoss();
    }
    */

    private void Awake()
    {
        /* 실험용 타겟 강제지정
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
        }
      
        */

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
    }


    public void RegisterBoss(BossController boss)
    {
        CurrentBoss = boss;
    }

    public void UnregisterBoss()
    {
        CurrentBoss = null;
    }

    public bool IsBossAlive => CurrentBoss != null;
}
