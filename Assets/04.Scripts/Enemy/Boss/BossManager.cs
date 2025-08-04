using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [Header("���� ����")]
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
            Debug.LogWarning("BossManager: bossPrefab�� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        CurrentBoss = bossObj.GetComponent<BossController>();
        bossHpObject.SetActive(true);

        if (CurrentBoss == null)
        {
            Debug.LogWarning("BossManager: BossController ������Ʈ�� �������� �ʽ��ϴ�.");
            return;
        }

        if (playerTarget == null)
        {
            Debug.LogWarning("BossManager: playerTarget�� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        CurrentBoss.Init(this, playerTarget);
        // EnemyManager���� activeEnemies ���
        EnemyManager.Instance.activeEnemies.Add(CurrentBoss);
    }

    public void UnregisterBoss()
    {
        CurrentBoss = null;
    }

}
