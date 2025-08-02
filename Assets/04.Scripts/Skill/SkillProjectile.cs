using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public float speed = 5f; // 발사체 속도
    public float damage = 10f; // 발사체 피해량
    private Vector2 direction; // 발사체 방향

    public void Init(Vector2 dir, float dmg, float spd)
    {
        direction = dir.normalized; // 방향 벡터를 정규화
        damage = dmg; // 피해량 설정
        speed = spd; // 속도 설정

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 발사체 이동
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

            Destroy(gameObject); // 적과 충돌 시 발사체 제거
        }
    }
}
