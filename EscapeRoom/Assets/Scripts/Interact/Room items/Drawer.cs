using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Drawer : Interactable
    {
        Animator animator;

        bool isOpen = false;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        protected override void Trigger(bool isActive)
        {
            base.Trigger(isActive);
            if (!isActive) return;

            if (isOpen)
            {
                animator.SetTrigger("closeDrawer");
                isOpen = false;
            }
            else
            {
                animator.SetTrigger("openDrawer");
                isOpen = true;
            }

            SetIsInteractable(false);
        }

        public void FinishAnimation()
        {
            SetIsInteractable(true);
        }
    }
}

