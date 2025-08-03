using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BossBaseController
{
    private BossManager bossManager;
    private BossAnimationHandler animHandler;
    [SerializeField] private GameObject warningAreaPrefab;
    [SerializeField] private float meleeAttackRange = 2.5f;
    [SerializeField] private float warningDuration = 2f;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float rangedExplosionRadius = 1f;
    [SerializeField] private float projectileHeight = 5f; // 위로 얼마나 쏠지
    [SerializeField] private float fallDelay = 2f;
    [SerializeField] private float fallSpeed = 3f;
    [SerializeField] private int rangedDamage = 20;

    private bool isDying = false;

    public void Init(BossManager bossManager, Transform target)
    {
        this.bossManager = bossManager;
        this.target = target;
        animHandler = GetComponent<BossAnimationHandler>();

        if (animHandler == null)
        {
            Debug.LogWarning("BossAnimationHandler가 없습니다!");
        }

        StartCoroutine(BossBehaviorPattern());
    }

    private IEnumerator BossBehaviorPattern()
    {
        yield return new WaitForSeconds(2f); // 등장 후 대기

        while (true)
        {
            // 원거리 범위공격 (투사체 생성 → 터짐)
            yield return StartCoroutine(RangedAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // 플레이어에게 돌진
            yield return StartCoroutine(MoveTowardsPlayer(2f));

            // 근거리 범위공격 (즉시 폭발)
            yield return StartCoroutine(MeleeAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // 플레이어 반대방향으로 30도 틀어서 이동
            yield return StartCoroutine(MoveAtAngleFromPlayer(1.4f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator MoveProjectile(GameObject proj, Vector3 from, Vector3 to, float duration)
{
    float t = 0f;
    while (t < duration)
    {
        proj.transform.position = Vector3.Lerp(from, to, t / duration);
        t += Time.deltaTime;
        yield return null;
    }
    proj.transform.position = to;
}

    private IEnumerator RangedAreaAttack()
    {
        Debug.Log("보스: 원거리 범위공격");

        Vector3 targetPos = target.position;

        animHandler.Skill();

        // 1. 경고 범위 생성
        GameObject warning = Instantiate(warningAreaPrefab, targetPos, Quaternion.identity);
        warning.transform.localScale = new Vector3(rangedExplosionRadius, rangedExplosionRadius, 1);

        // 2. 보스 발사 → 위로 상승
        Vector3 shootUpPos = firePoint.position + Vector3.up * projectileHeight;
        GameObject riseProj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        float riseTime = 1f;
        yield return MoveProjectile(riseProj, firePoint.position, shootUpPos, riseTime);
        Destroy(riseProj); // 위로 간 후 제거

        // 3. 경고 유지시간 대기
        yield return new WaitForSeconds(warningDuration);

        // 4. 위에서 낙하할 투사체 생성
        Vector3 fallStartPos = targetPos + Vector3.up * projectileHeight;
        GameObject fallProj = Instantiate(projectilePrefab, fallStartPos, Quaternion.identity);

        float fallTime = projectileHeight / fallSpeed;
        yield return MoveProjectile(fallProj, fallStartPos, targetPos, fallTime);
        Destroy(fallProj); // 낙하 후 제거

        // 5. 폭발 이펙트
        Instantiate(explosionEffectPrefab, targetPos, Quaternion.identity);

        // 6. 데미지 판정
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPos, rangedExplosionRadius / 2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                player?.TakeDamage(rangedDamage);
                Debug.Log("보스: 플레이어에게 원거리 폭발 데미지 입힘");
            }
        }

        // 7. 정리
        Destroy(warning);
        yield return new WaitForSeconds(1f); // 후딜레이
    }

    private IEnumerator MeleeAreaAttack()
    {
        Debug.Log("보스: 근거리 범위공격 준비");

        // 1. 경고 이펙트 생성
        GameObject warning = Instantiate(warningAreaPrefab, transform.position, Quaternion.identity);
        warning.transform.localScale = new Vector3(meleeAttackRange, meleeAttackRange, 1);

        // 2. 경고 지속시간 기다림
        yield return new WaitForSeconds(warningDuration);

        animHandler.Attack();

        // 3. 범위 안에 있는 타겟들에게 데미지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange / 2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(AtkPower);
                    Debug.Log("보스: 플레이어에게 근거리 데미지 입힘");
                }
            }
        }

        // 4. 이펙트 제거
        Destroy(warning);

        // 후딜레이
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator MoveTowardsPlayer(float duration)
    {
        animHandler.Move();
        Debug.Log("보스: 플레이어에게 접근");
        float t = 0f;
        while (t < duration)
        {
            UpdateFacingDirection();
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
        animHandler.Idle();
    }

    private IEnumerator MoveAtAngleFromPlayer(float duration)
    {
        animHandler.Move();
        Debug.Log("보스: 플레이어 반대 방향으로 도망");

        // 플레이어를 등진 방향 계산
        Vector3 awayDir = (transform.position - target.position).normalized;

        float t = 0f;
        while (t < duration)
        {
            UpdateFacingDirection();
            transform.position += awayDir * moveSpeed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
        animHandler.Idle();
    }

    private void UpdateFacingDirection()
    {
        if (target == null) return;

        // 방향 비교
        if (target.position.x < transform.position.x)
        {
            characterRenderer.flipX = true;  // 왼쪽에 있으면 왼쪽 바라보게
        }
        else
        {
            characterRenderer.flipX = false; // 오른쪽에 있으면 오른쪽 바라보게
        }
    }

    public void Die()
    {
        if (!isDying)
        {
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        isDying = true;

        if (animHandler != null)
        {
            animHandler.Death(); // 죽는 애니메이션 시작
        }

        Debug.Log("보스: 죽는 애니메이션 재생 중 (6초 대기)");
        yield return new WaitForSeconds(6f); // 애니메이션 연출 시간 대기

        bossManager?.UnregisterBoss();
        Destroy(gameObject);
    }
}
