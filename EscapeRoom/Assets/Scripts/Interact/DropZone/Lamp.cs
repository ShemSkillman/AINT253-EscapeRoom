using EscapeRoom.Core;
using EscapeRoom.Interact.Item;
using EscapeRoom.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Drop
{
    public class Lamp : DropZone
    {
        [SerializeField] float delayBetweenColourChange = 2f;
        [SerializeField] float delayBetweenSequence = 4f;

        public delegate void OnObjectiveComplete(Objective objective);
        public event OnObjectiveComplete onScrewInBulb;

        public override bool IsInteractionValid()
        {
            InventoryItem item = inventory.GetSelectedItem();

            if (item == null || item.Item == null) return false;

            Bulb bulb = item.Item.GetComponent<Bulb>();

            if (bulb == null) return false;

            ShowItemGhost();
            return true;
        }

        protected override void Trigger(bool isActive)
        {
            if (!isActive) return;

            InventoryItem item = inventory.GetSelectedItem();

            inventory.RemoveItem(item);

            GameObject instance = Instantiate(item.Item, ghostSpawnLocation.position, ghostSpawnLocation.rotation);

            Bulb bulb = instance.GetComponent<Bulb>();
            bulb.SetIsInteractable(false);

            onScrewInBulb(Objective.LookForLightBulb);

            StartCoroutine(LightSequence(bulb));
        }

        private IEnumerator LightSequence(Bulb bulb)
        {
            Colour[] colourSequence = FindObjectOfType<Bookshelf>().GetColourCode();

            while(true)
            {
                for(int i = 0; i < 7; i++)
                {
                    bulb.SetLight(colourSequence[i]);

                    yield return new WaitForSeconds(delayBetweenColourChange);
                }

                bulb.SetLight(Colour.None);
                yield return new WaitForSeconds(delayBetweenSequence);
            }
        }
    }
}

