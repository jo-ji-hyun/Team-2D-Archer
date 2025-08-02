using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField] private GameObject[] MagicPrefabs;

    // === �̱��� ���� ===
    private static ShootManager instance;
    public static ShootManager Instance { get { return instance; } }

    private StatsManager _stats_Manager;

    private SkillManager _skill_Manager;

    private void Awake()
    {
        instance = this;
    }
    // -----------------------

    // === �߽� ���� ===
    public void ShootMagic(RangeWeapon rangeWeapon, Vector2 startPostiion, Vector2 direction, MagicCodex magic)
    {
        GameObject origin = MagicPrefabs[magic.magicIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        Shoot shoot = obj.GetComponent<Shoot>();

        shoot.Init(direction,rangeWeapon, this._stats_Manager, this, this._skill_Manager);
    }

    // ���� �Ŵ����� ������
    public void GiveRange(StatsManager stats, SkillManager skill)
    {
        this._stats_Manager = stats;
        this._skill_Manager = skill;
    }
}
