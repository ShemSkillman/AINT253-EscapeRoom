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
            if (timeText == null) return;
            StartCoroutine(Clock());
        }

        IEnumerator Clock()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                timeElapsed += 1;

                timeText.text = GetTime();
            }
        }

        public string GetTime()
        {
            int seconds = timeElapsed;

            int minutes = seconds / 60;
            seconds -= minutes * 60;

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public string ConvertTime(int seconds)
        {
            int minutes = seconds / 60;
            seconds -= minutes * 60;

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public int GetTimeSeconds()
        {
            return timeElapsed;
        }
    }

}

