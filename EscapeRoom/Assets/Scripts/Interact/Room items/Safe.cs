using EscapeRoom.Interact;
using System;
using UnityEngine;

namespace EscapeRoom.Item
{
    public class Safe : ClickInteractable
    {
        [SerializeField] int codeCombination = 1234;
        [SerializeField] int attempt = -1;
        [SerializeField] float openDelay = 2f;

        Animator animator;

        bool unlocked = false;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        protected override void SendButtonID(int id)
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

        protected void OpenSafe()
        {
            animator.SetTrigger("openSafe");
        }

    }
}

