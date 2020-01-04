using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeRoom.Core
{
    public class GameManager : MonoBehaviour
    {
        public void NextScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            currentIndex++;

            SceneManager.LoadScene(currentIndex);
            Time.timeScale = 1f;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

