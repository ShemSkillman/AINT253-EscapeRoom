using EscapeRoom.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EscapeRoom.UI
{
    public class HighscoreDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI highScoresText;

        Timer timer;
        HighScores highScores;

        private void Awake()
        {
            timer = GetComponent<Timer>();
        }

        private void Start()
        {
            string s = PlayerPrefs.GetString("HighScores");

            if (string.IsNullOrEmpty(s))
            {
                highScoresText.text = "No one has managed to escape yet!";
                return;
            }

            highScores = JsonUtility.FromJson<HighScores>(s);
            List<PlayerScore> scores = highScores.scores;

            for (int i = 0; i < scores.Count; i++)
            {
                string playerName = scores[i].playerName;
                string timeTaken = timer.ConvertTime(scores[i].timeTaken);
                
                highScoresText.text += (i + 1).ToString() + ") " + playerName + " - " + timeTaken + "\n";
            }
        }
    }
    
}

