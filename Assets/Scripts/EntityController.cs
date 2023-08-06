using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public System.Action OnAttackEnded;
    public Character currentData;
    private Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }
}
