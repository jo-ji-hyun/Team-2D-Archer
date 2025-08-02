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
        yield return new WaitForSeconds(2f); // �� ���� �� ���

        while (true)
        {
            // �� ���Ÿ� �������� (����ü ���� �� ����)
            yield return StartCoroutine(RangedAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �� �÷��̾�� ����
            yield return StartCoroutine(MoveTowardsPlayer(1f));

            // �� �ٰŸ� �������� (��� ����)
            yield return StartCoroutine(MeleeAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �� �÷��̾� �ݴ�������� 30�� Ʋ� �̵�
            yield return StartCoroutine(MoveAtAngleFromPlayer(2f, 30f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator RangedAreaAttack()
    {
        // TODO: ���Ÿ� ����ü ���� + ���� �ð� �� ����
        Debug.Log("����: ���Ÿ� ��������");
        // ��:Instantiate(projectile, firePosition, Quaternion.identity)
        yield return new WaitForSeconds(1f); // ���� �ð�
    }

    private IEnumerator MeleeAreaAttack()
    {
        Debug.Log("����: �ٰŸ� �������� �غ�");

        // 1. ��� ����Ʈ ����
        GameObject warning = Instantiate(warningAreaPrefab, transform.position, Quaternion.identity);
        warning.transform.localScale = new Vector3(meleeAttackRange, meleeAttackRange, 1);

        // 2. ��� ���ӽð� ��ٸ�
        yield return new WaitForSeconds(warningDuration);

        // 3. ���� �ȿ� �ִ� Ÿ�ٵ鿡�� ������
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, meleeAttackRange / 2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(AtkPower);
                    Debug.Log("����: �÷��̾�� �ٰŸ� ������ ����");
                }
            }
        }

        // 4. ����Ʈ ����
        Destroy(warning);

        // �ĵ�����
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator MoveTowardsPlayer(float duration)
    {
        Debug.Log("����: �÷��̾�� ����");
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
        Debug.Log($"����: �÷��̾� ���� 30�� �������� �̵�");
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

        // ���� ��
        if (target.position.x < transform.position.x)
        {
            characterRenderer.flipX = true;  // ���ʿ� ������ ���� �ٶ󺸰�
        }
        else
        {
            characterRenderer.flipX = false; // �����ʿ� ������ ������ �ٶ󺸰�
        }
    }

    public void Die()
    {
        bossManager.UnregisterBoss();
        Destroy(gameObject);
    }
}
