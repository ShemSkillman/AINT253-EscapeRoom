using EscapeRoom.Interact;
using UnityEngine;

namespace EscapeRoom.Item
{
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
}

