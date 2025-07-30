using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponHandler
{
    // === 생성 위치 ===
    [SerializeField] private Transform SpawnPosition;

    // === 마법 종류 0부터 ===
    [SerializeField] private int magicIndex = 0;
    public int MagicIndex { get { return magicIndex; } }

    // === 마법 크기 ===
    [SerializeField] private float magicSize = 1;
    public float MagicSize { get { return magicSize; } }

    // === 지속 시간 ===
    [SerializeField] private float duration = 10;
    public float Duration { get { return duration; } }

    // === 구체 속도 ===
    [SerializeField] private float spread = 1;
    public float Spread { get { return spread; } }

    // === 생성 갯수 ===
    [SerializeField] private int shotNumber = 1;
    public int ShotNumber { get { return shotNumber; } }

    // === 다중샷 각도 ===
    [SerializeField] private float multipleAngel = 1;
    public float MultipleAngel { get { return multipleAngel; } }

    // === 마법 색깔 === (혹시 모르니)
    [SerializeField] private Color magicColor;
    public Color MagicColor { get { return magicColor; } }

    public override void Attack()
    {
        base.Attack(); // 애니메이션 가져오기

        float AngleSpace = multipleAngel; // 각도
        int PerShot = ShotNumber;          // 생성 갯수

        // === 각도 조절 ===
        float minAngle = -(PerShot / 2f) * AngleSpace;

        for (int i = 0; i < PerShot; i++)
        {
            float angle = minAngle + AngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {

    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }
}
