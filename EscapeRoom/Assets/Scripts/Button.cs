using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] int id;

    // Cache references
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PushButton()
    {
        animator.SetTrigger("buttonPush");
    }

    public int GetID()
    {
        return id;
    }
}
