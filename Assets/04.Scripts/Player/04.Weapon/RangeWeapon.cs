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

    // === ���� ===
    [SerializeField] private float spread = 0.3f;
    public float Spread { get { return spread; } }

    // === ���� ���� ===
    private ShootManager _shoot_Manager;
    public MagicCodex _magic_Codex;

    private SkillManager _skill_Manager;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;

        _magic_Codex = new MagicCodex();
    }

    public void Init(SkillManager skill)
    {
        this._skill_Manager = skill;
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
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateMagicShoot(Controller.LookDirection, angle);

            if (_skill_Manager.acquiredSkills.Count > 0)
            {
                CreateMagic(Controller.LookDirection, angle); // ���簡���� �ִ� ��ų�� �Ѱ� �̻��� ���
            }
        }
    }

    // === ���� ����ü ���� ���� ===
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }

    // === �⺻ ���� �߻� ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), _magic_Codex);
    }

    // === ���� �ߵ� ===
    private void CreateMagic(Vector2 _lookDirection, float angle)
    {
        _skill_Manager.UseSkill(SpawnPosition.position, RotateVector2(_lookDirection, angle), _skill_Manager.acquiredSkills.Count);
    }

}
