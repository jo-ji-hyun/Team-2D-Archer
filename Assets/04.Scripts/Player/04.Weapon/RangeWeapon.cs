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

    // === 다중샷 각도 ===
    [SerializeField] private float multipleAngel = 15;
    public float MultipleAngel { get { return multipleAngel; } }

    // === 퍼짐 ===
    [SerializeField] private float spread = 1;
    public float Spread { get { return spread; } }

    // === 마법 참조 ===
    private ShootManager _shoot_Manager;
    public MagicCodex _magic_Codex;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;

        _magic_Codex = new MagicCodex();
    }

    public override void Attack()
    {
        base.Attack(); // 공격시작

        float AngleSpace = multipleAngel; // 각도
        int PerShot = _magic_Codex.shotNumber;          // 생성 갯수

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

    // === 마법 투사체 각도 조절 ===
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }

    // === 마법 발사 ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), _magic_Codex);
    }

}
