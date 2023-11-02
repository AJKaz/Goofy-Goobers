using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMainLevel() {
        SceneManager.LoadScene("Main Level");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
