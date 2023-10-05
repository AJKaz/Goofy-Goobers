using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;

    private Grid grid;
    private GameObject[,] towers;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(9, 9, 1, new Vector3(-4.5f, -4.5f));
        towers = new GameObject[grid.width, grid.height];
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            int gridX, gridY;
            grid.GetXY(worldMousePosition, out gridX, out gridY);
            // If the tower prefab exists and there is not currently a tower there, create one
            if(towerPrefab != null && towers[gridX, gridY] == null)
            {
                GameObject tower = Instantiate(towerPrefab, grid.GetWorldPosition(gridX, gridY) + new Vector3(grid.cellSize / 2f, grid.cellSize / 2f), Quaternion.identity);
                tower.transform.SetParent(transform, true);
                towers[gridX, gridY] = tower;
            }
        }
        else if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            int gridX, gridY;
            grid.GetXY(worldMousePosition, out gridX, out gridY);
            // If a tower is there, destroy it
            if (towers[gridX, gridY] != null)
            {
                Destroy(towers[gridX, gridY]);
            }
        }
    }
}
