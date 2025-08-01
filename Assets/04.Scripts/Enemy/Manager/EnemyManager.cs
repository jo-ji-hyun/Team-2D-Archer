using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable] public class EnemyWave
{
    public string waveName;
    public List<EnemySpawnInfo> enemySpawns;
}

[System.Serializable] public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public int count;
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Wave Settings")]
    [SerializeField] private List<EnemyWave> waves = new List<EnemyWave>();
    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    [Header("Spawn Area Settings")]
    [SerializeField] private List<Rect> spawnAreas; // ���� ������ ���� ����Ʈ
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����


    public List<EnemyBaseController> activeEnemies = new List<EnemyBaseController>();
    private Transform playerTarget;
    private Coroutine waveRoutine;

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    // === ��� �Ž����� �߰�
    private bool _enemy_Spawn_Complete;
    public bool _is_Enemy_Spawn_Complete => _enemy_Spawn_Complete;

    private void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("EnemyManager: 'Player' �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�! �÷��̾ �±װ� �����Ǿ� �ִ��� Ȯ���ϼ���.");
        }
       
    }

    public bool AllEnemiesDead()
    {
        // Ȱ��ȭ�� ���� ������ true ��ȯ
        return activeEnemies.Count == 0;
    }

    public void StartWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= waves.Count)
        {
            Debug.LogWarning($"Wave index {waveIndex}�� ������ ������ϴ�.");
            return;
        }

        if (waveRoutine != null) StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waves[waveIndex]));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(EnemyWave wave)
    {
        yield return new WaitForSeconds(5f); // 5�� ���

        _enemy_Spawn_Complete = false;
        foreach (var spawnInfo in wave.enemySpawns)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                SpawnEnemy(spawnInfo.enemyPrefab);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

        _enemy_Spawn_Complete = true;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        if (spawnAreas.Count == 0)
        {
            Debug.LogWarning("Spawn Area�� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        // ������ ���� ����
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect ���� ������ ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );


        // �� ���� �� ����Ʈ�� �߰�
        GameObject spawnedEnemy = Instantiate(prefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        var baseController = spawnedEnemy.GetComponent<EnemyBaseController>();

        if (baseController == null)
        {
            Debug.LogWarning("EnemyBaseController ������Ʈ�� ����: " + spawnedEnemy.name);
            return;
        }

        // Init �Լ� ȣ�� (�ڽ� Ŭ������ �°� �ڵ����� ȣ���)
        if (playerTarget != null)
        {
            // �ڽ� Ŭ�������� Init ȣ��
            if (baseController is EnemyController melee)
            {
                melee.Init(this, playerTarget);
            }
            else if (baseController is EnemyRangedController ranged)
            {
                ranged.Init(this, playerTarget);
            }
        }

        activeEnemies.Add(baseController); // ���� ����Ʈ�� �߰�
    }

    public void SpawnBoss()
    {
        var bossObj = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        var boss = bossObj.GetComponent<BossController>();
        if (boss != null && playerTarget != null)
            boss.Init(this, playerTarget);
    }

    // ����� �׷� ������ �ð�ȭ (���õ� ��쿡�� ǥ��)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    private void Update()
    {
        
    }

    public bool IsAllSpawned => _enemy_Spawn_Complete;
}
