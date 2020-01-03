using UnityEngine;
using TMPro;
using System.Collections;

namespace EscapeRoom.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;

        int timeElapsed = 0;

        private void Start()
        {
            StartCoroutine(Clock());
        }

        IEnumerator Clock()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                timeElapsed += 1;

                int seconds = timeElapsed;

                int hours = seconds / 3600;
                seconds -= hours * 3600;

                int minutes = seconds / 60;
                seconds -= minutes * 60;

                timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            }            
        }
    }
}

