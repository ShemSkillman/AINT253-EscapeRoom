using EscapeRoom.Interact.Item;
using EscapeRoom.UI;
using UnityEngine;

namespace EscapeRoom.Interact
{
    public class BookSlot : Interactable
    {
        [SerializeField] GameObject invisibleBook;
        InventoryUI inventoryUI;
        

        protected override void Awake()
        {
            inventoryUI = FindObjectOfType<InventoryUI>();
        }

        public override bool IsInteractionValid()
        {
            if (inventoryUI.GetSelectedItem() == null) return false;

            if (inventoryUI.GetSelectedItem().GetComponent<Book>() != null)
            {
                invisibleBook.SetActive(true);
                return true;
            }
            else return false;
        }
    }
}

