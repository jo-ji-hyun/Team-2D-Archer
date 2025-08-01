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
    [SerializeField] private List<Rect> spawnAreas; // 적을 생성할 영역 리스트
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상


    public List<EnemyBaseController> activeEnemies = new List<EnemyBaseController>();
    private Transform playerTarget;
    private Coroutine waveRoutine;

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    // === 경고문 거슬려서 추가
    private bool _enemy_Spawn_Complete;
    public bool _is_Enemy_Spawn_Complete => _enemy_Spawn_Complete;

    private void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("EnemyManager: 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다! 플레이어에 태그가 지정되어 있는지 확인하세요.");
        }
       
    }

    public bool AllEnemiesDead()
    {
        // 활성화된 적이 없으면 true 반환
        return activeEnemies.Count == 0;
    }

    public void StartWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= waves.Count)
        {
            Debug.LogWarning($"Wave index {waveIndex}는 범위를 벗어났습니다.");
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
        yield return new WaitForSeconds(5f); // 5초 대기

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
            Debug.LogWarning("Spawn Area가 설정되어 있지 않습니다.");
            return;
        }

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );


        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(prefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        var baseController = spawnedEnemy.GetComponent<EnemyBaseController>();

        if (baseController == null)
        {
            Debug.LogWarning("EnemyBaseController 컴포넌트가 없음: " + spawnedEnemy.name);
            return;
        }

        // Init 함수 호출 (자식 클래스에 맞게 자동으로 호출됨)
        if (playerTarget != null)
        {
            // 자식 클래스별로 Init 호출
            if (baseController is EnemyController melee)
            {
                melee.Init(this, playerTarget);
            }
            else if (baseController is EnemyRangedController ranged)
            {
                ranged.Init(this, playerTarget);
            }
        }

        activeEnemies.Add(baseController); // 공통 리스트에 추가
    }

    public void SpawnBoss()
    {
        var bossObj = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        var boss = bossObj.GetComponent<BossController>();
        if (boss != null && playerTarget != null)
            boss.Init(this, playerTarget);
    }

    // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
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
