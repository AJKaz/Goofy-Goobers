using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Grid))]
public class PlacementIndicator : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    private Vector2 mousePosition;
    int gridX, gridY;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        grid.GetXY(mousePosition, out gridX, out gridY);
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0)
        {

            Debug.DrawLine(grid.GetWorldPosition(gridX, gridY), grid.GetWorldPosition(gridX + 1, gridY), Color.green);
            Debug.DrawLine(grid.GetWorldPosition(gridX + 1, gridY), grid.GetWorldPosition(gridX + 1, gridY + 1), Color.green);
            Debug.DrawLine(grid.GetWorldPosition(gridX + 1, gridY + 1), grid.GetWorldPosition(gridX, gridY + 1), Color.green);
            Debug.DrawLine(grid.GetWorldPosition(gridX, gridY + 1), grid.GetWorldPosition(gridX, gridY), Color.green);
        }
    }
}
