using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            animator.SetTrigger("action1");
        if(Input.GetKeyDown(KeyCode.Alpha2))
            animator.SetTrigger("action2");
        if(Input.GetKeyDown(KeyCode.Alpha3))
            animator.SetTrigger("action3");
    }
}
