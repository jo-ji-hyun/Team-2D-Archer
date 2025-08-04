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
    [SerializeField] private GameObject rangedExplosionEffectPrefab;
    [SerializeField] private GameObject meleeExplosionEffectPrefab;
    [SerializeField] private float rangedExplosionRadius = 1f;
    [SerializeField] private float projectileHeight = 5f; // ���� �󸶳� ����
    [SerializeField] private float fallDelay = 2f;
    [SerializeField] private float fallSpeed = 3f;
    [SerializeField] private int rangedDamage = 20;
    [SerializeField] private Transform attackPivot;

    private bool isDying = false;

    protected override void Awake()
    {
        base.Awake();
        IsBoss = true;
    }

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
        yield return new WaitForSeconds(2f); // ���� �� ���

        while (true)
        {
            // ���Ÿ� �������� (����ü ���� �� ����)
            yield return StartCoroutine(RangedAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �÷��̾�� ����
            yield return StartCoroutine(MoveTowardsPlayer(2f));

            // �ٰŸ� �������� (��� ����)
            yield return StartCoroutine(MeleeAreaAttack());

            yield return new WaitForSeconds(0.5f);

            // �÷��̾� �ݴ�������� 30�� Ʋ� �̵�
            yield return StartCoroutine(MoveAtAngleFromPlayer(1.4f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator MoveProjectile(GameObject proj, Vector3 from, Vector3 to, float duration)
{
    float t = 0f;
    while (t < duration)
    {
        if (proj == null) yield break;
        proj.transform.position = Vector3.Lerp(from, to, t / duration);
        t += Time.deltaTime;
        yield return null;
    }
    proj.transform.position = to;
}

    private IEnumerator RangedAreaAttack()
    {
        Debug.Log("����: ���Ÿ� ��������");

        Vector3 targetPos = target.position;

        animHandler.Skill();

        // 1. ��� ���� ����
        Vector3 warningPos = targetPos + new Vector3(0, 2f, 0);
        warningPos.z = 0f; // 2D�� ��� z ����
        GameObject warning = Instantiate(warningAreaPrefab, warningPos, Quaternion.identity);
        warning.transform.localScale = new Vector3(rangedExplosionRadius, rangedExplosionRadius, 1);

        // 2. ���� �߻� �� ���� ���
        Vector3 shootUpPos = firePoint.position + Vector3.up * projectileHeight;
        GameObject riseProj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        SpriteRenderer sr = riseProj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipY = true;
        }

        float riseTime = 1f;
        yield return MoveProjectile(riseProj, firePoint.position, shootUpPos, riseTime);
        Destroy(riseProj); // ���� �� �� ����

        // 3. ��� �����ð� ���
        yield return new WaitForSeconds(warningDuration);

        // 4. ������ ������ ����ü ����
        Vector3 fallStartPos = targetPos + Vector3.up * projectileHeight;
        GameObject fallProj = Instantiate(projectilePrefab, fallStartPos, Quaternion.identity);

        float fallTime = projectileHeight / fallSpeed;
        yield return MoveProjectile(fallProj, fallStartPos, targetPos, fallTime);
        Destroy(fallProj); // ���� �� ����

        // 5. ���� ����Ʈ
        GameObject explosion = Instantiate(rangedExplosionEffectPrefab, targetPos, Quaternion.identity);

        // 6. ������ ����
        PolygonCollider2D polyCol = warning.GetComponent<PolygonCollider2D>();
        if (polyCol != null)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Player")); // �Ǵ� ��ü ���̾�� ����
            filter.useTriggers = true;

            Collider2D[] results = new Collider2D[10];
            int count = polyCol.OverlapCollider(filter, results);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = results[i];
                if (hit != null && hit.CompareTag("Player"))
                {
                    PlayerController player = hit.GetComponent<PlayerController>();
                    player?.TakeDamage(rangedDamage);
                    Debug.Log("����: ���Ÿ� ������ - ������ �ȿ� ����");
                }
            }
        }

        // 7. ����      
        Destroy(explosion, 2f);
        Destroy(warning);

        yield return new WaitForSeconds(1f); // �ĵ�����
    }

    private IEnumerator MeleeAreaAttack()
    {
        Debug.Log("����: �ٰŸ� �������� �غ�");

        Vector3 attackCenter = attackPivot.position;

        // 1. ��� ����Ʈ ����
        Vector3 warningPos = attackPivot.position + new Vector3(0, 2f, 0);
        GameObject warning = Instantiate(warningAreaPrefab, warningPos, Quaternion.identity);
        warning.transform.localScale = new Vector3(meleeAttackRange, meleeAttackRange, 1);

        // 2. ��� ���ӽð� ��ٸ�
        yield return new WaitForSeconds(warningDuration);

        animHandler.Attack();

        // 3. ���� ����Ʈ ����
        GameObject explosion = Instantiate(meleeExplosionEffectPrefab, warningPos + new Vector3(0, -2f, 0), Quaternion.identity);

        // 4. ���� �ȿ� �ִ� Ÿ�ٵ鿡�� ������
        PolygonCollider2D polyCol = warning.GetComponent<PolygonCollider2D>();
        if (polyCol != null)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Player")); // �Ǵ� ��ü ���̾�� ����
            filter.useTriggers = true;

            Collider2D[] results = new Collider2D[10];
            int count = polyCol.OverlapCollider(filter, results);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = results[i];
                if (hit != null && hit.CompareTag("Player"))
                {
                    PlayerController player = hit.GetComponent<PlayerController>();
                    player?.TakeDamage(BossAtkPower);
                    Debug.Log("����: �ٰŸ� ������ - ������ �ȿ� ����");
                }
            }
        }

        // 5. ����Ʈ ����
        Destroy(explosion, 2f); // 2�� �� �ı�
        Destroy(warning);

        // �ĵ�����
        yield return new WaitForSeconds(1f);
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

    private new void UpdateFacingDirection()
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

    public new void Die()
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
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
