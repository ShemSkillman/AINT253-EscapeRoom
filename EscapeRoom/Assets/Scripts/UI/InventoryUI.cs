using EscapeRoom.Interact;
using EscapeRoom.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeRoom.UI
{
    public class InventoryUI : MonoBehaviour
    {
        Inventory inventory;
        Hands hands;
        
        [SerializeField] Button previousPageButton;
        [SerializeField] Button nextPageButton;
        int slotsPerPage;

        int currentPageIndex = 0;
        List<InventoryItem> items = new List<InventoryItem>();
        int pageCount;
        InventorySlot[] slots;
        InventoryItem selectedItem;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
            slots = GetComponentsInChildren<InventorySlot>();
            hands = FindObjectOfType<Hands>();
        }

        private void Start()
        {
            slotsPerPage = slots.Length;

            UpdateDisplay();
        }

        private void OnEnable()
        {
            inventory.onInventoryChange += UpdateDisplay;
        }

        private void OnDisable()
        {
            inventory.onInventoryChange -= UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            items = inventory.GetItems();

            if (items.Count < 1)
            {
                UpdateCurrentPage();
                return;
            }
            
            pageCount = items.Count / slotsPerPage;

            if (items.Count % slotsPerPage != 0) pageCount++;

            CheckPages();
            UpdateCurrentPage();

        }

        private void UpdateCurrentPage()
        {
            int itemIndex = currentPageIndex * slotsPerPage;

            for (int i = 0; i < slotsPerPage; i++, itemIndex++)
            {
                Toggle slotToggle = slots[i].GetComponent<Toggle>();
                Image slotImage = slots[i].GetComponent<Image>();

                if (itemIndex >= items.Count)
                {
                    slotImage.sprite = null;
                    slotToggle.interactable = false;
                }
                else
                {
                    slotImage.sprite = items[itemIndex].Icon;
                    slotToggle.interactable = true;
                }
            }
        }

        public void ChangeItemSelected(bool isSelected, int slotNumber)
        {
            int pageItemIndex = slotNumber - 1;

            int itemIndex = currentPageIndex * slotsPerPage + pageItemIndex;
            GameObject itemPrefab = items[itemIndex].Item;

            if (isSelected)
            {
                hands.HoldItem(itemPrefab);
                selectedItem = items[itemIndex];
            }
            else
            {
                hands.EmptyHands();
                selectedItem = items[itemIndex];
            }
        }

        public void PreviousPage()
        {
            if (currentPageIndex == 0) return;

            currentPageIndex--;
            UpdateCurrentPage();

            CheckPages();
        }

        public void NextPage()
        {
            if (currentPageIndex + 1 >= pageCount) return;

            currentPageIndex++;
            UpdateCurrentPage();

            CheckPages();
        }

        private void CheckPages()
        {
            if (currentPageIndex < 0) currentPageIndex = 0;
            if (currentPageIndex >= pageCount) currentPageIndex = pageCount - 1;

            if (currentPageIndex == 0) previousPageButton.interactable = false;
            else previousPageButton.interactable = true;

            if (currentPageIndex + 1 >= pageCount) nextPageButton.interactable = false;
            else nextPageButton.interactable = true;
        }

        public GameObject GetSelectedItem()
        {
            if (selectedItem == null) return null;
            return selectedItem.Item;
        }
    }

}
