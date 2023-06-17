using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            animator.SetTrigger("attack");
        }
    }
    }
