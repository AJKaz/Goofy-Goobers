using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame() {
        //SceneManager.LoadSceneAsync(1); 
        // game scene is index 1
        SceneManager.LoadScene("BuildScene");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
