using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // 발사체 속도
    public float lifetime = 2.0f; // 자동 파괴 시간

    private Vector2 direction = Vector2.right; // 초기 발사 방향(오른쪽)
    void Start()
    {
        Destroy(gameObject, lifetime); // 일정 시간 후 자동 파괴
    }

    // Update is called once per frame
    void Update()
    {
        // direction 방향으로 이동
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // 방향 지정 함수(생성 후 방향 설정 가능)
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // 충돌 시(2D)
    void OnTriggerEnter2D(Collider2D other)
    {
        // 예시: 적, 벽 등에 부딪치면 파괴
        Destroy(gameObject);
    }
}
