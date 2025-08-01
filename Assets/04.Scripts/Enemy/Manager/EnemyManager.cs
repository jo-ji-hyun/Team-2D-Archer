using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField]
    private List<GameObject> enemyPrefabs; // ������ �� ������ ����Ʈ

    [SerializeField]
    private List<Rect> spawnAreas; // ���� ������ ���� ����Ʈ

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����

    private List<EnemyBaseController> activeEnemies = new List<EnemyBaseController>();

    // === ��� �Ž����� �߰�
    private bool _enemy_Spawn_Complete;
    public bool _is_Enemy_Spawn_Complete 
    {
        get { return _enemy_Spawn_Complete; }
        private set { _enemy_Spawn_Complete = value; } // ���ο����� ���� ����, �ܺο����� �б⸸ ����
    }

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    private Transform playerTarget;

    private void Awake()
    {
        
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

    public void StartWave(int waveCount)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        _is_Enemy_Spawn_Complete = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        _is_Enemy_Spawn_Complete = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ������ �� ������ ����
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // ������ ���� ����
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect ���� ������ ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // �� ���� �� ����Ʈ�� �߰�
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyBaseController baseController = spawnedEnemy.GetComponent<EnemyBaseController>();
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
        else
        {
            Debug.LogWarning("�÷��̾� Ÿ���� ã�� ���߽��ϴ�.");
        }

        activeEnemies.Add(baseController); // ���� ����Ʈ�� �߰�
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWave(1);
        }
    }
}
