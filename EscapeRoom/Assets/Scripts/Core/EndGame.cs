using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EscapeRoom.Core
{
    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int timeTaken;

        public PlayerScore(string playerName, int timeTaken)
        {
            this.playerName = playerName;
            this.timeTaken = timeTaken;
        }
    }

    public class HighScores
    {
        public List<PlayerScore> scores;
    }

    public class EndGame : MonoBehaviour
    {
        [SerializeField] Canvas gameEndCanvas;
        [SerializeField] TextMeshProUGUI timeTakenText;
        [SerializeField] TextMeshProUGUI playerNameInputText;
        [SerializeField] int maxNumberOfHighScores = 10;
        [SerializeField] int maxNameLength = 20;

        CanvasSwitcher canvasSwitcher;
        EndGameCollider[] colliders;

        HighScores highScores;

        private void Awake()
        {
            canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
            colliders = GetComponentsInChildren<EndGameCollider>();
        }

        private void OnEnable()
        {
            foreach (EndGameCollider collider in colliders)
            {
                collider.onDetectPlayer += GameSuccess;
            }
        }

        private void OnDisable()
        {
            foreach (EndGameCollider collider in colliders)
            {
                collider.onDetectPlayer -= GameSuccess;
            }
        }

        private void GameSuccess()
        {
            canvasSwitcher.SwitchToThisCanvas(gameEndCanvas);
            Time.timeScale = 0f;
            Cursor.visible = true;

            timeTakenText.text = "Time taken\n" + GetComponent<Timer>().GetTime();
        }

        public void SaveScore()
        {
            string s = PlayerPrefs.GetString("HighScores");

            // Get player details
            int timeTaken = GetComponent<Timer>().GetTimeSeconds();
            string playerName = FixName(playerNameInputText.text);

            // Checks for previous highscores
            if (string.IsNullOrEmpty(s))
            {
                highScores = new HighScores();
                highScores.scores = new List<PlayerScore>();
            }
            else 
            {
                highScores = JsonUtility.FromJson<HighScores>(s);
            }

            // If highscore table full compare player time to worst highscore time
            if (highScores.scores.Count == maxNumberOfHighScores)
            {
                int worstTimeTaken = highScores.scores[maxNumberOfHighScores - 1].timeTaken;

                if (worstTimeTaken > timeTaken)
                {
                    highScores.scores[maxNumberOfHighScores - 1] = new PlayerScore(playerName, timeTaken);
                }
            }
            else
            {
                highScores.scores.Add(new PlayerScore(playerName, timeTaken));
            }

            SortHighScores();

            string scoresJSON = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("HighScores", scoresJSON);
        }

        private string FixName(string name)
        {
            string newName = "";

            for(int i = 0; i < name.Length; i++)
            {
                if (i >= maxNameLength)
                {
                    newName += "...";
                    break;
                }

                if (name[i].Equals(' '))
                {
                    newName += "_";
                }
                else
                {
                    newName += name[i];
                }                
            }

            return newName;
        }

        private void SortHighScores()
        {
            List<PlayerScore> listToSort = highScores.scores;
            List<PlayerScore> sortedList = new List<PlayerScore>();

            while(listToSort.Count > 0)
            {
                PlayerScore bestSoFar = listToSort[0];

                for (int i = 0; i < listToSort.Count; i++)
                {
                    if (listToSort[i].timeTaken < bestSoFar.timeTaken)
                    {
                        bestSoFar = listToSort[i];
                    }
                }

                listToSort.Remove(bestSoFar);
                sortedList.Add(bestSoFar);
            }

            highScores.scores = sortedList;
        }
    }

}
