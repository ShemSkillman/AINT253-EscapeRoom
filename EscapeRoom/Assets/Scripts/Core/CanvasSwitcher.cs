using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Core
{
    public class CanvasSwitcher : MonoBehaviour
    {
        [SerializeField] Canvas defaultCanvas;
        Canvas currentlyActiveCanvas;

        private void Awake()
        {
            currentlyActiveCanvas = defaultCanvas;
        }

        public void SwitchToThisCanvas(Canvas canvasToShow)
        {
            currentlyActiveCanvas.gameObject.SetActive(false);
            currentlyActiveCanvas = canvasToShow;
            currentlyActiveCanvas.gameObject.SetActive(true);
        }

        public void SwitchBackToDefault()
        {
            currentlyActiveCanvas.gameObject.SetActive(false);
            currentlyActiveCanvas = defaultCanvas;
            currentlyActiveCanvas.gameObject.SetActive(true);
        }

        public bool GetIsDefaultCanvasActive()
        {
            return defaultCanvas.isActiveAndEnabled;
        }
    }
}

