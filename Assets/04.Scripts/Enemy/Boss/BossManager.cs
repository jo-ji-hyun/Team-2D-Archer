using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [Header("���� ����")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    private BossController currentBoss;
    private Transform playerTarget;

    public GameObject bossHpObject;

    /* ����� ��������
    private void Start()
    {
        SpawnBoss();
    }
    */

    private void Awake()
    {
        /* ����� Ÿ�� ��������
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
            Debug.LogWarning("BossManager: bossPrefab�� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        currentBoss = bossObj.GetComponent<BossController>();
        bossHpObject.SetActive(true);

        if (currentBoss == null)
        {
            Debug.LogWarning("BossManager: BossController ������Ʈ�� �������� �ʽ��ϴ�.");
            return;
        }

        if (playerTarget == null)
        {
            Debug.LogWarning("BossManager: playerTarget�� �����Ǿ� ���� �ʽ��ϴ�.");
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
