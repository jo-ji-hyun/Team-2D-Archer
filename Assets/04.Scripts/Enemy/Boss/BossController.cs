using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BossBaseController
{
    private BossManager bossManager;
    [SerializeField] private GameObject warningAreaPrefab;
    [SerializeField] private float meleeAttackRange = 2.5f;
    [SerializeField] private float warningDuration = 1.5f;

    public void Init(BossManager bossManager, Transform target)
    {
        this.target = target;
        StartCoroutine(BossBehaviorPattern());
    }

    private IEnumerator BossBehaviorPattern()
    {
        yield return new WaitForSeconds(2f); // ▶ 등장 후 대기

        while (true)
        {
            // ▶ 원거리 범위공격 (투사체 생성 → 터짐)
            yield return StartCoroutine(RangedAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // ▶ 플레이어에게 돌진
            yield return StartCoroutine(MoveTowardsPlayer(1f));

            // ▶ 근거리 범위공격 (즉시 폭발)
            yield return StartCoroutine(MeleeAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // ▶ 플레이어 반대방향으로 30도 틀어서 이동
            yield return StartCoroutine(MoveAtAngleFromPlayer(2f, 30f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator RangedAreaAttack()
    {
        // TODO: 원거리 투사체 생성 + 일정 시간 후 폭발
        Debug.Log("보스: 원거리 범위공격");
        // 예:Instantiate(projectile, firePosition, Quaternion.identity)
        yield return new WaitForSeconds(1f); // 연출 시간
    }

    private IEnumerator MeleeAreaAttack()
    {
        Debug.Log("보스: 근거리 범위공격 준비");

        // 1. 경고 이펙트 생성
        GameObject warning = Instantiate(warningAreaPrefab, transform.position, Quaternion.identity);
        warning.transform.localScale = new Vector3(meleeAttackRange, meleeAttackRange, 1);

        // 2. 경고 지속시간 기다림
        yield return new WaitForSeconds(warningDuration);

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
        Debug.Log("보스: 플레이어에게 접근");
        float t = 0f;
        while (t < duration)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveAtAngleFromPlayer(float duration, float angleOffset)
    {
        Debug.Log($"보스: 플레이어 기준 30도 방향으로 이동");
        Vector3 toPlayer = (target.position - transform.position).normalized;
        Vector3 rotatedDir = Quaternion.Euler(0, 0, angleOffset) * toPlayer;

        float t = 0f;
        while (t < duration)
        {
            transform.position += rotatedDir * moveSpeed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
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
        bossManager.UnregisterBoss();
        Destroy(gameObject);
    }
}
