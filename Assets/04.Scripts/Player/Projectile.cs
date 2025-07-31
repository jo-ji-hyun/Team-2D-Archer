using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // �߻�ü �ӵ�
    public float lifetime = 2.0f; // �ڵ� �ı� �ð�

    private Vector2 direction = Vector2.right; // �ʱ� �߻� ����(������)
    void Start()
    {
        Destroy(gameObject, lifetime); // ���� �ð� �� �ڵ� �ı�
    }

    // Update is called once per frame
    void Update()
    {
        // direction �������� �̵�
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // ���� ���� �Լ�(���� �� ���� ���� ����)
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // �浹 ��(2D)
    void OnTriggerEnter2D(Collider2D other)
    {
        // ����: ��, �� � �ε�ġ�� �ı�
        Destroy(gameObject);
    }
}
