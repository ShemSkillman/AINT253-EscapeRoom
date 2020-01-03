using EscapeRoom.Interact.Drop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Book : Pickup
    {
        [SerializeField] Colour bookColour;

        BookSlot bookSlot;
        int childIndex;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            bookSlot = GetComponentInParent<BookSlot>();
        }

        protected override void Trigger(bool isActive)
        {
            if (!isActive) return;

            bookSlot.RemoveBook();
            bookSlot = null;

            base.Trigger(isActive);
        }

        public void SetBookSlot(BookSlot bookSlot)
        {
            this.bookSlot = bookSlot;
        }

        public Colour GetBookColour()
        {
            return bookColour;
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
    Red,
    None
}

