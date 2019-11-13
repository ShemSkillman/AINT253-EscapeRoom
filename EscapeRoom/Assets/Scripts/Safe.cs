using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IControllable
{
    [SerializeField] int codeCombination = 1234;
    [SerializeField] int attempt = -1;
    [SerializeField] float openDelay = 2f;

    Animator animator;

    bool unlocked = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void sendButtonID(int id)
    {
        if (unlocked) return;
        
        if (attempt == -1)
        {
            attempt = id;
        }
        else
        {
            attempt = Convert.ToInt32(attempt.ToString() + id.ToString());
        }

        if (attempt == codeCombination)
        {
            unlocked = true;
            Invoke("OpenSafe", openDelay);
            return;
        }
        else if (attempt.ToString().Length >= codeCombination.ToString().Length)
        {
            attempt = -1;
        }
    }

    private void OpenSafe()
    {
        animator.SetTrigger("openSafe");

    }
    
}
