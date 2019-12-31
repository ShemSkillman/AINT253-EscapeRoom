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
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

