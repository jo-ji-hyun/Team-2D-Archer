using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public Animator animator; // �ִϸ��̼��� ����� Animator ������Ʈ

    void Start()
    {
        animator.Play("Flamethrower"); // "Flamethrower" �ִϸ��̼��� ���.
        Destroy(gameObject, 1f); // 1�� �� �� ������Ʈ�� ������ �ı�.
    }

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

}
