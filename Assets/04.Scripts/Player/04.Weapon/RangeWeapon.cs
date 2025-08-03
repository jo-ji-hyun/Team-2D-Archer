using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponHandler
{
    // === 생성 위치 ===
    [SerializeField] private Transform SpawnPosition;

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

            CreateMagicShoot(Controller.LookDirection, angle); // 기본 무기 발싸
        }

        // === 스킬을 소유할 경우 ===
        if (_skill_Manager.acquiredSkills.Count > 0)
        {
            for (int i = 0; i < _skill_Manager.acquiredSkills.Count; i++)
            {
                var skillData = _skill_Manager.acquiredSkills[i];

                // === 쿨타임이 다 된 스킬이 있는 경우 ===
                if (skillData.currentCoolTime <= 0) 
                {
                    float angle = minAngle + AngleSpace * i; // 스킬별 각도 조정
                    float randomSpread = Random.Range(-spread, spread);
                    angle += randomSpread;

                    CreateMagic(Controller.LookDirection, angle, i + 1);
                    skillData.currentCoolTime = skillData.coolTime; // 쿨타임 초기화
                }
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
    private void CreateMagic(Vector2 _lookDirection, float angle, int idx)
    {
        _skill_Manager.UseSkill(this, SpawnPosition.position, RotateVector2(_lookDirection, angle), idx);
    }

}
