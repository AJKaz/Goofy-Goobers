using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPanelControl : MonoBehaviour
{
    [SerializeField] private Canvas BuildingPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBuild(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Performed)
        {
            BuildingPanel.enabled = !BuildingPanel.enabled;
            Debug.Log("B");
        }
    }
}
