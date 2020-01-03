using EscapeRoom.Core;
using EscapeRoom.Interact.Drop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Bookshelf : MonoBehaviour
    {
        [SerializeField] float bookshelfMoveDelay = 1f;
        [SerializeField] List<Book> bookPrefabs;
        [SerializeField] Transform bookSlotsParent;

        Colour[] currentBookColourOrder;
        Colour[] colourCode;
        List<Colour> allColours = new List<Colour>();
        bool isSetUp = false;

        public delegate void OnObjectiveComplete(Objective objective);
        public event OnObjectiveComplete onSequenceSolved;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            int numBooks = bookPrefabs.Count;
            currentBookColourOrder = new Colour[numBooks];
            colourCode = new Colour[numBooks];

            SetUpBooks();
            GenerateRandomColourCode();
        }        

        private void SetUpBooks()
        {
            BookSlot[] bookSlots = bookSlotsParent.GetComponentsInChildren<BookSlot>();

            foreach (BookSlot slot in bookSlots)
            {
                int randomIndex = UnityEngine.Random.Range(0, bookPrefabs.Count);
                Book randomBook = bookPrefabs[randomIndex];
                bookPrefabs.RemoveAt(randomIndex);

                Book instance = Instantiate(randomBook, slot.transform.position, slot.transform.rotation, slot.transform);
                slot.PlaceBook(randomBook);

                allColours.Add(instance.GetBookColour());
            }

            isSetUp = true;
        }

        private void GenerateRandomColourCode()
        {
            for (int i = 0; i < colourCode.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, allColours.Count);
                Colour randomColour = allColours[randomIndex];
                allColours.RemoveAt(randomIndex);

                colourCode[i] = randomColour;
            }

            if (CheckSequenceCorrect()) GenerateRandomColourCode();
        }

        public void SetBookColourOrder(int bookIndex, Colour colour)
        {
            currentBookColourOrder[bookIndex] = colour;

            if (!isSetUp) return;

            bool solved = CheckSequenceCorrect();
            if (solved)
            {
                onSequenceSolved(Objective.ArrangeBooks);
                Invoke("PlayAnimation", bookshelfMoveDelay);
            }
        }

        private void PlayAnimation()
        {
            animator.SetTrigger("bookshelfMove");
        }

        private bool CheckSequenceCorrect()
        {
            for(int i = 0; i < 7; i++)
            {
                Colour colour = currentBookColourOrder[i];

                if (colour != colourCode[i]) return false;
            }

            return true;
        }

        public Colour[] GetColourCode()
        {
            return colourCode;
        }
    }
}

