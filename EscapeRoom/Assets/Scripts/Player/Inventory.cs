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
        InventoryItem selectedItem;
        Hands hands;

        public delegate void OnInventoryChange();
        public event OnInventoryChange onInventoryChange;

        private void Awake()
        {
            hands = GetComponent<Hands>();
        }

        public void AddItem(GameObject itemPrefab, Sprite inventoryIcon)
        {
            items.Add(new InventoryItem(itemPrefab, inventoryIcon));

            onInventoryChange?.Invoke();
        }

        public void RemoveItem(InventoryItem item)
        {
            if (!items.Contains(item)) return;

            items.Remove(item);
            hands.EmptyHands();
            selectedItem = null;

            onInventoryChange?.Invoke();
        }

        public List<InventoryItem> GetItems()
        {
            return items;
        }

        public void SelectItem(int itemIndex, bool isSelected)
        {
            if (itemIndex >= items.Count) return;
            GameObject itemPrefab = items[itemIndex].Item;

            if (isSelected)
            {
                hands.HoldItem(itemPrefab);
                selectedItem = items[itemIndex];
            }
            else
            {
                hands.EmptyHands();
                selectedItem = null;
            }
        }

        public InventoryItem GetSelectedItem()
        {
            return selectedItem;
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

