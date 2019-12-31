using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Book : Pickup
    {
        [SerializeField] Colour bookColour;

        Bookshelf bookShelf;
        int childIndex;

        protected override void Awake()
        {
            base.Awake();
            bookShelf = GetComponentInParent<Bookshelf>();
        }

        protected override void Start()
        {
            childIndex = transform.GetSiblingIndex();
            base.Start();
            bookShelf.PlaceBook(bookColour, childIndex);
        }

        protected override void Trigger(bool isActive)
        {
            bookShelf.PickUpBook(childIndex);

            base.Trigger(isActive);
            if (!isActive) return;

        }
    }    
}

public enum Colour
{
    Blue,
    Yellow,
    Orange,
    Green,
    Purple,
    Pink,
    Red
}

