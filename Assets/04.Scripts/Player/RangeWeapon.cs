using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponHandler
{
    // === ���� ��ġ ===
    [SerializeField] private Transform SpawnPosition;

    // === ���� ������ �ִ� ���� ���� ===
    [SerializeField] public int magicCount = 2;
    public int MagicCount { get { return magicCount; } }

    // === ���� ���� 0���� ===
    [SerializeField] public int magicIndex = 0;
    public int MagicIndex { get { return magicIndex; } }

    // === ���� ũ�� ===
    [SerializeField] public float magicSize = 3.0f;
    public float MagicSize { get { return magicSize; } }

    // === ���� �ð� ===
    [SerializeField] private float duration = 5.0f;
    public float Duration { get { return duration; } }

    // === ��ü �ӵ� ===
    [SerializeField] private float spread = 1.0f;
    public float Spread { get { return spread; } }

    // === ���� ���� ===
    [SerializeField] private int shotNumber = 1;
    public int ShotNumber { get { return shotNumber; } }

    // === ���߼� ���� ===
    [SerializeField] private float multipleAngel = 15;
    public float MultipleAngel { get { return multipleAngel; } }

    // === ���� ���� ===
    private ShootManager _shoot_Manager;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;
    }

    public override void Attack()
    {
        base.Attack(); // ���ݽ���

        float AngleSpace = multipleAngel; // ����
        int PerShot = ShotNumber;          // ���� ����

        // === ���� ���� ===
        float minAngle = -(PerShot / 2f) * AngleSpace;

        for (int i = 0; i < PerShot; i++)
        {
            float angle = minAngle + AngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateMagicShoot(Controller.LookDirection, angle);
        }
    }

    // === ���� �߻� ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        for (int i = 0; i < magicCount; i++) // ���� �������ִ� ���� ��
        {
            magicIndex = i; // ����� index�� ���� ���� ���� ���� �߽��ϵ��� ��
            _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle));
        }

    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }
}
