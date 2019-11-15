using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInParent<Animator>();
    }
    protected override void Trigger(bool isActive)
    {
        if (isActive) OpenDoor();
    }

    private void OpenDoor()
    {
        animator.SetTrigger("openDoor");
    }
}
