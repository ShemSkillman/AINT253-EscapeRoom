using EscapeRoom.Player;
using UnityEngine;

namespace EscapeRoom.Interact
{
    public class Pickup : Interactable
    {
        [SerializeField] Sprite inventoryIcon;
        [SerializeField] GameObject itemPrefab;
        
        Inventory inventory;

        protected override void Awake()
        {
            base.Awake();
            inventory = FindObjectOfType<Inventory>();
        }

        protected override void Trigger(bool isActive)
        {
            if (!isActive) return;
            inventory.AddItem(itemPrefab, inventoryIcon);
            Destroy(gameObject);
        }
    }
}


