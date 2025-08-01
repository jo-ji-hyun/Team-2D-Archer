using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponHandler
{
    // === 생성 위치 ===
    [SerializeField] private Transform SpawnPosition;

    // === 현재 가지고 있는 무기 갯수 ===
    [SerializeField] public int magicCount = 2;
    public int MagicCount { get { return magicCount; } }

    // === 마법 종류 0부터 ===
    [SerializeField] public int magicIndex = 0;
    public int MagicIndex { get { return magicIndex; } }

    // === 마법 크기 ===
    [SerializeField] public float magicSize = 3.0f;
    public float MagicSize { get { return magicSize; } }

    // === 지속 시간 ===
    [SerializeField] private float duration = 5.0f;
    public float Duration { get { return duration; } }

    // === 구체 속도 ===
    [SerializeField] private float spread = 1.0f;
    public float Spread { get { return spread; } }

    // === 생성 갯수 ===
    [SerializeField] private int shotNumber = 1;
    public int ShotNumber { get { return shotNumber; } }

    // === 다중샷 각도 ===
    [SerializeField] private float multipleAngel = 15;
    public float MultipleAngel { get { return multipleAngel; } }

    // === 마법 참조 ===
    private ShootManager _shoot_Manager;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;
    }

    public override void Attack()
    {
        base.Attack(); // 공격시작

        float AngleSpace = multipleAngel; // 각도
        int PerShot = ShotNumber;          // 생성 갯수

        // === 각도 조절 ===
        float minAngle = -(PerShot / 2f) * AngleSpace;

        for (int i = 0; i < PerShot; i++)
        {
            float angle = minAngle + AngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateMagicShoot(Controller.LookDirection, angle);
        }
    }

    // === 마법 발사 ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        for (int i = 0; i < magicCount; i++) // 현재 가지고있는 무기 수
        {
            magicIndex = i; // 저장된 index의 값을 점점 높여 같이 발싸하도록 함
            _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle));
        }

    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }
}
