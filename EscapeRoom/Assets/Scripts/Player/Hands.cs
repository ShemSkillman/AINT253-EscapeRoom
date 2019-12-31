using UnityEngine;

namespace EscapeRoom.Player
{
    public class Hands : MonoBehaviour
    {
        [SerializeField] Transform hand;

        GameObject currentlyHeldItem;

        public void HoldItem(GameObject item)
        {
            if (item == currentlyHeldItem) return;

            EmptyHands();

            currentlyHeldItem = Instantiate(item, hand.position, hand.rotation, hand);
            Collider[] itemColliders = currentlyHeldItem.GetComponentsInChildren<Collider>();

            foreach (Collider collider in itemColliders)
            {
                collider.enabled = false;
            }
        }

        public void EmptyHands()
        {
            if (currentlyHeldItem != null) Destroy(currentlyHeldItem);
        }
    }
}

