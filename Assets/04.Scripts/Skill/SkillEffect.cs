using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public Animator animator; // 애니메이션을 재생할 Animator 컴포넌트

    void Start()
    {
        animator.Play("Flamethrower"); // "Flamethrower" 애니메이션을 재생.
        Destroy(gameObject, 1f); // 1초 후 이 오브젝트를 씬에서 파괴.
    }

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

}
