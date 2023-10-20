using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 
    public void PlayGame() {
        //SceneManager.LoadSceneAsync(1); // game scene is index 1
        SceneManager.LoadSceneAsync("BuildScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
