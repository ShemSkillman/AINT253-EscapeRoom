using EscapeRoom.Interact.Item;
using EscapeRoom.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Drop
{
    public class BookSlot : DropZone
    {
        Book book;
        Collider GhostBookCollider;
        Bookshelf bookShelf;

        protected override void Awake()
        {
            base.Awake();
            GhostBookCollider = GetComponent<Collider>();
            bookShelf = GetComponentInParent<Bookshelf>();
        }

        private void Update()
        {
            if (book == null)
            {
                GhostBookCollider.enabled = true;
            }
            else
            {
                GhostBookCollider.enabled = false;
            }
        }

        public override bool IsInteractionValid()
        {
            InventoryItem item = inventory.GetSelectedItem();

            if (item == null || item.Item == null) return false;

            Book book = item.Item.GetComponent<Book>();

            if (book == null) return false;

            ShowItemGhost();
            return true;
        }

        protected override void Trigger(bool isActive)
        {
            if (!isActive) return;

            InventoryItem item = inventory.GetSelectedItem();

            inventory.RemoveItem(item);

            PlaceBook(item.Item.GetComponent<Book>());

            Book instance = Instantiate(book, transform.position, transform.rotation, transform);

            instance.SetItemPrefab(book.gameObject);
        }

        public void PlaceBook(Book book)
        {
            if (book == null) return;

            bookShelf.SetBookColourOrder(transform.GetSiblingIndex(), book.GetBookColour());
            this.book = book;
        }
        
        public void RemoveBook()
        {
            bookShelf.SetBookColourOrder(transform.GetSiblingIndex(), Colour.None);
            book = null;
        }
    }
}

