using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Bookshelf : MonoBehaviour
    {
        [SerializeField] float bookshelfMoveDelay = 1f;
        [Header("Colour code")]
        [SerializeField] Colour slotOneBookColour;
        [SerializeField] Colour slotTwoBookColour;
        [SerializeField] Colour slotThreeBookColour;
        [SerializeField] Colour slotFourBookColour;
        [SerializeField] Colour slotFiveBookColour;
        [SerializeField] Colour slotSixBookColour;
        [SerializeField] Colour slotSevenBookColour;

        Colour[] currentBookColourOrder = new Colour[7];
        Colour[] colourCode;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            colourCode = new Colour[] { slotOneBookColour, slotTwoBookColour, slotThreeBookColour, slotFourBookColour,
                slotFiveBookColour, slotSixBookColour, slotSevenBookColour};

            for (int i = 0; i < 7; i++)
            {
                currentBookColourOrder[i] = Colour.None;
            }
        }

        public void SetBookColourOrder(int bookIndex, Colour colour)
        {
            currentBookColourOrder[bookIndex] = colour;

            bool solved = CheckSequenceCorrect();

            if (solved) Invoke("PlayAnimation", bookshelfMoveDelay);
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

