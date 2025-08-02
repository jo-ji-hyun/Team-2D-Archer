using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BossBaseController
{
    private BossManager bossManager;
    private BossAnimationHandler animHandler;
    [SerializeField] private GameObject warningAreaPrefab;
    [SerializeField] private float meleeAttackRange = 2.5f;
    [SerializeField] private float warningDuration = 1.5f;
    private bool isDying = false;

    public void Init(BossManager bossManager, Transform target)
    {
        this.bossManager = bossManager;
        this.target = target;
        animHandler = GetComponent<BossAnimationHandler>();

        if (animHandler == null)
        {
            Debug.LogWarning("BossAnimationHandler�� �����ϴ�!");
        }

        StartCoroutine(BossBehaviorPattern());
    }

    private IEnumerator BossBehaviorPattern()
    {
        yield return new WaitForSeconds(2f); // �� ���� �� ���

        while (true)
        {
            // ���Ÿ� �������� (����ü ���� �� ����)
            yield return StartCoroutine(RangedAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �� �÷��̾�� ����
            yield return StartCoroutine(MoveTowardsPlayer(2f));

            // �� �ٰŸ� �������� (��� ����)
            yield return StartCoroutine(MeleeAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �� �÷��̾� �ݴ�������� 30�� Ʋ� �̵�
            yield return StartCoroutine(MoveAtAngleFromPlayer(1.4f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator RangedAreaAttack()
    {
        // TODO: ���Ÿ� ����ü ���� + ���� �ð� �� ����
        Debug.Log("����: ���Ÿ� ��������");
        animHandler.Skill();
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

        animHandler.Attack();

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
        animHandler.Move();
        Debug.Log("����: �÷��̾�� ����");
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
        Debug.Log("����: �÷��̾� �ݴ� �������� ����");

        // �÷��̾ ���� ���� ���
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
            animHandler.Death(); // �״� �ִϸ��̼� ����
        }

        Debug.Log("����: �״� �ִϸ��̼� ��� �� (6�� ���)");
        yield return new WaitForSeconds(6f); // �ִϸ��̼� ���� �ð� ���

        bossManager?.UnregisterBoss();
        Destroy(gameObject);
    }
}
