using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    private List<EnemyBaseController> activeEnemies = new List<EnemyBaseController>();

    // === 경고문 거슬려서 추가
    private bool _enemy_Spawn_Complete;
    public bool _is_Enemy_Spawn_Complete 
    {
        get { return _enemy_Spawn_Complete; }
        private set { _enemy_Spawn_Complete = value; } // 내부에서는 설정 가능, 외부에서는 읽기만 가능
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
            Debug.LogWarning("EnemyManager: 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다! 플레이어에 태그가 지정되어 있는지 확인하세요.");
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
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyBaseController baseController = spawnedEnemy.GetComponent<EnemyBaseController>();
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
        else
        {
            Debug.LogWarning("플레이어 타겟을 찾지 못했습니다.");
        }

        activeEnemies.Add(baseController); // 공통 리스트에 추가
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWave(1);
        }
    }
}
