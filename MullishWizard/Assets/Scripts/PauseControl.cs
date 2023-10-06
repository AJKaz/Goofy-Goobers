using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    public static bool isPaused = true;

    [SerializeField] private Canvas pauseMenu;

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
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0.0f : 1.0f;
            pauseMenu.enabled = isPaused;
        }
    }
}
