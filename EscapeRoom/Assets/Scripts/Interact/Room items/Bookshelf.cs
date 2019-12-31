using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Bookshelf : MonoBehaviour
    {
        [SerializeField] Transform booksParent;
        [SerializeField] Transform slotsParent;

        Colour[] bookColourOrder = new Colour[7];
        bool[] bookSpaceOccupied = new bool[7];

        private void Start()
        {
            for(int i = 0; i < bookSpaceOccupied.Length; i++)
            {
                bookSpaceOccupied[i] = true;
            }
        }

        public void PickUpBook(int bookIndex)
        {
            print("picking up book at " + bookIndex);
            bookSpaceOccupied[bookIndex] = false;

            slotsParent.GetChild(bookIndex).gameObject.SetActive(true);
        }

        public void PlaceBook(Colour bookColour, int bookIndex)
        {
            bookSpaceOccupied[bookIndex] = true;

            slotsParent.GetChild(bookIndex).gameObject.SetActive(false);

            bookColourOrder[bookIndex] = bookColour;
        }
    }
}

