using EscapeRoom.Core;
using EscapeRoom.Interact;
using EscapeRoom.Interact.Item;
using EscapeRoom.Player;
using UnityEngine;

namespace EscapeRoom.Item
{
    public class Door : Interactable
    {
        Animator animator;
        Inventory inventory;

        public delegate void OnObjectiveComplete(Objective objective);
        public event OnObjectiveComplete onDoorOpen;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponentInParent<Animator>();
            inventory = FindObjectOfType<Inventory>();
        }

        public override bool IsInteractionValid()
        {
            InventoryItem item = inventory.GetSelectedItem();

            if (item == null || item.Item == null) return false;

            Key key = item.Item.GetComponent<Key>();

            if (key == null) return false;
            return true;
        }

        protected override void Trigger(bool isActive)
        {
            if (isActive) OpenDoor();

            if (!isActive) return;

            InventoryItem item = inventory.GetSelectedItem();

            inventory.RemoveItem(item);

            onDoorOpen(Objective.OpenDoor);
        }

        private void OpenDoor()
        {
            animator.SetTrigger("openDoor");
        }
    }
}

