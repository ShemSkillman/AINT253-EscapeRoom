using UnityEngine;
using UnityEngine.UI;

namespace EscapeRoom.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] int slotNumber;

        InventoryUI inventoryUI;
        Toggle toggle;

        private void Awake()
        {
            inventoryUI = GetComponentInParent<InventoryUI>();
            toggle = GetComponent<Toggle>();
        }

        private void Update()
        {
            if (Input.GetKeyDown((KeyCode)slotNumber + 48) && toggle.interactable)
            {
                toggle.isOn = !toggle.isOn;
            }
        }

        public void SlotChange(bool isSelected)
        {
            inventoryUI.ChangeItemSelected(isSelected, slotNumber);
        }
    }
}

