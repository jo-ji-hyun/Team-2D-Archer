using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public float speed = 5f; // �߻�ü �ӵ�
    public float damage = 10f; // �߻�ü ���ط�
    private Vector2 direction; // �߻�ü ����

    public void Init(Vector2 dir, float dmg, float spd)
    {
        direction = dir.normalized; // ���� ���͸� ����ȭ
        damage = dmg; // ���ط� ����
        speed = spd; // �ӵ� ����

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // �߻�ü �̵�
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyBaseController>();
            if (enemy != null)
            {
                enemy.statHandler.TakeDamage(damage);
            }

            Destroy(gameObject); // ���� �浹 �� �߻�ü ����
        }
    }
}
