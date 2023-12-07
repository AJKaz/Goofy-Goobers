using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
