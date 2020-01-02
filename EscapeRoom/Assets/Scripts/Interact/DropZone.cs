using EscapeRoom.Interact.Item;
using EscapeRoom.Player;
using EscapeRoom.UI;
using UnityEngine;

namespace EscapeRoom.Interact
{
    public class DropZone : Interactable
    {
        [SerializeField] protected GameObject itemGhostPrefab;
        [SerializeField] protected Transform ghostSpawnLocation;
        protected Inventory inventory;

        protected GameObject itemGhost;

        protected override void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        protected virtual void ShowItemGhost()
        {
            if (itemGhost != null) return;

            Transform spawnTransform = transform;
            if (ghostSpawnLocation != null) spawnTransform = ghostSpawnLocation;

            itemGhost = Instantiate(itemGhostPrefab, spawnTransform.position, spawnTransform.rotation);
        }

        protected virtual void RemoveItemGhost()
        {
            if (itemGhost == null) return;

            Destroy(itemGhost);
        }

        public override void FinishInteraction()
        {
            RemoveItemGhost();
        }
    }
}

