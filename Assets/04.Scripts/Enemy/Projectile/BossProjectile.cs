using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float explosionRadius = 2f;
    public int damage = 10;
    private Vector3 targetPosition;

    public void Init(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                var player = hit.GetComponent<PlayerController>();
                player?.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
