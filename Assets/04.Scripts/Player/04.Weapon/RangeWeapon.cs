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

    // === 스피드 ===
    [SerializeField] private float speed = 1f;
    public float Speed { get { return speed;  } }

    // === 다중샷 각도 ===
    [SerializeField] private float multipleAngel = 15;
    public float MultipleAngel { get { return multipleAngel; } }

    // === 퍼짐 ===
    [SerializeField] private float spread = 0.3f;
    public float Spread { get { return spread; } }

    // === 스킬 쿨타임 ===
    public float skillCoolTime;

    // === 마법 참조 ===
    private ShootManager _shoot_Manager;
    public MagicCodex _magic_Codex;

    private SkillManager _skill_Manager;

    protected override void Start()
    {
        base.Start();
        _shoot_Manager = ShootManager.Instance;

        // === 기본 무기 참조 ===
        _magic_Codex = new MagicCodex();
    }

    protected override void Update()
    {
        base.Update();

        if (skillCoolTime > 0f)
        {
            skillCoolTime -= Time.deltaTime;
        }
        else
        {
            skillCoolTime = 0f;
        }
    }

    public void Init(SkillManager skill)
    {
        this._skill_Manager = skill;
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
            speed = _magic_Codex.speed;

            CreateMagicShoot(Controller.LookDirection, angle); // 기본 무기 발싸
        }

        // === 현재 가지고있는 스킬이 있는 경우 && 스킬 쿨타임이 다되었을 경우 ===
        if (_skill_Manager.acquiredSkills.Count > 0 && skillCoolTime <= 0)
        {
            for (int i = 0; i < 1; i++) // 나중에 멀티샷 같은거 추가시 최대 i 수치를 변경합시다.
            {
                float angle = minAngle + AngleSpace;
                float randomSpread = Random.Range(-spread, spread);
                angle += randomSpread;
                speed += _magic_Codex.speed;

                CreateMagic(Controller.LookDirection, angle); // 현재가지고 있는 스킬이 한개 이상일 경우
                skillCoolTime = 3.1f;
            }
        }
    }

    // === 마법 투사체 각도 조절 ===
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;

    }

    // === 기본 무기 발사 ===
    private void CreateMagicShoot(Vector2 _lookDirection, float angle)
    {
        _shoot_Manager.ShootMagic(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), _magic_Codex);
    }

    // === 마법 발동 ===
    private void CreateMagic(Vector2 _lookDirection, float angle)
    {
        _skill_Manager.UseSkill(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), _skill_Manager.acquiredSkills.Count);
    }

}
