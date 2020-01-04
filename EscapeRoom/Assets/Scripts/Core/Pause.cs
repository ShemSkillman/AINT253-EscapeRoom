using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Core
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] Canvas pauseMenuCanvas;
        [SerializeField] Canvas controlsCanvas;

        CanvasSwitcher canvasSwitcher;

        bool isPaused = false;

        private void Awake()
        {
            canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
        }

        private void Start()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (canvasSwitcher.GetIsDefaultCanvasActive() == false && !isPaused) return;
                GamePause();
            }
        }

        public void GamePause()
        {
            isPaused = !isPaused;

            if (isPaused) canvasSwitcher.SwitchToThisCanvas(pauseMenuCanvas);
            else canvasSwitcher.SwitchBackToDefault();

            Cursor.visible = isPaused;

            if (isPaused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
        }

        public void ShowControls()
        {
            canvasSwitcher.SwitchToThisCanvas(controlsCanvas);
        }

        public void ShowPauseMenu()
        {
            canvasSwitcher.SwitchToThisCanvas(pauseMenuCanvas);
        }
    }
}

