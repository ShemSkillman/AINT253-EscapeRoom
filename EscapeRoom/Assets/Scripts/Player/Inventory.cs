using EscapeRoom.Interact;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeRoom.Player
{
    public class Inventory : MonoBehaviour
    {
        List<InventoryItem> items = new List<InventoryItem>();

        public delegate void OnInventoryChange();
        public event OnInventoryChange onInventoryChange;

        public void AddItem(GameObject itemPrefab, Sprite inventoryIcon)
        {
            items.Add(new InventoryItem(itemPrefab, inventoryIcon));

            onInventoryChange?.Invoke();
        }

        public void RemoveItem(InventoryItem item)
        {
            if (!items.Contains(item)) return;

            items.Remove(item);

            onInventoryChange?.Invoke();
        }

        public List<InventoryItem> GetItems()
        {
            return items;
        }
    }

    public class InventoryItem
    {
        public GameObject Item { get; }
        public Sprite Icon { get;  }

        public InventoryItem(GameObject itemPrefab, Sprite inventoryIcon)
        {
            Item = itemPrefab;
            Icon = inventoryIcon;
        }
    }
}

