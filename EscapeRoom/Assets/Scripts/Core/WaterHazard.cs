using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeRoom.Core
{
    public class WaterHazard : MonoBehaviour
    {
        [SerializeField] Canvas gameFailCanvas;
        [SerializeField] float holdBreathTime = 5f;

        CanvasSwitcher canvasSwitcher;

        private void Awake()
        {
            canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
        }


        public void SeaLevelRiseComplete()
        {
            canvasSwitcher.SwitchToThisCanvas(gameFailCanvas);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}

