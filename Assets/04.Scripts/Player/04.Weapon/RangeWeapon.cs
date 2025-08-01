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

    // === ���߼� ���� ===
    [SerializeField] private float multipleAngel = 15;
    public float MultipleAngel { get { return multipleAngel; } }

    // === ���� ���� ===
    private ShootManager _shoot_Manager;
    private MaficCodex _magic_Codex;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;

        _magic_Codex = new MaficCodex();
    }

    public override void Attack()
    {
        base.Attack(); // ���ݽ���

        float AngleSpace = multipleAngel; // ����
        int PerShot = _magic_Codex.shotNumber;          // ���� ����

        // === ���� ���� ===
        float minAngle = -(PerShot / 2f) * AngleSpace;

        for (int i = 0; i < PerShot; i++)
        {
            float angle = minAngle + AngleSpace * i;
            float randomSpread = Random.Range(-_magic_Codex.spread, _magic_Codex.spread);
            angle += randomSpread;
            CreateMagicShoot(Controller.LookDirection, angle);
        }
    }

    // === ���� �߻� ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        for (int i = 0; i < magicCount; i++) // ���� �������ִ� ���� ��
        {
            _magic_Codex.magicIndex = i; // ����� index�� ���� ���� ���� ���� �߽��ϵ��� ��
            _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), _magic_Codex);
        }

    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }
}
