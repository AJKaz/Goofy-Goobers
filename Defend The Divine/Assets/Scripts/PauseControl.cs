using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private bool pauseOnAwake = isPaused;    
    

    private Canvas pauseMenu;

    void Awake()
    {
        pauseMenu = GetComponent<Canvas>();
        Pause(pauseOnAwake);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPause(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            Pause(!isPaused);
        }
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0.0f : 1.0f;
        pauseMenu.enabled = isPaused;
    }
}