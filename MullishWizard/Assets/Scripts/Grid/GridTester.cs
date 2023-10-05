using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridTester : MonoBehaviour
{
    private Grid grid;
    void Start()
    {
        grid = new Grid(9, 9, 1, new Vector3(-4.5f, -4.5f));   
    }

    private void Update()
    {
        // Upon clicking the mouse, change the value of the containing grid cell, if any
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            grid.SetValue(worldMousePosition, 1);
        }
    }
}
