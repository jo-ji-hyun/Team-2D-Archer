using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public Vector3 direction;

    void Start()
    {
        Destroy(gameObject, 5f); // 5�� �� �ڵ� ����
    }

    void Update()
    {
        // �̵� ������ �������� ȸ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 150;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // ������ ����
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            else if (collision.CompareTag("object"))
            {
                Destroy(gameObject); // �� ���� �ſ� �ε����� ����
            }

            Destroy(gameObject);
        }
    }
}
