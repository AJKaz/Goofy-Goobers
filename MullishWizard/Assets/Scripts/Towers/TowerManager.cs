using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Grid))]
public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ThingSpawner spawner;

    private GameObject[,] towers;

    void Start()
    {
        towers = new GameObject[grid.width, grid.height];
    }

    public void CreateTower(GameObject towerPrefab, int gridX, int gridY)
    {
        // If grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0)
        {
            // If the tower prefab exists and there is not currently a tower there, create one
            if (towerPrefab != null && towers[gridX, gridY] == null)
            {
                GameObject tower = Instantiate(towerPrefab, grid.GetWorldPosition(gridX, gridY) + new Vector3(grid.cellSize / 2f, grid.cellSize / 2f), Quaternion.identity);
                //tower.GetComponent<Tower>().spawner = spawner;
                tower.transform.SetParent(transform, true);
                towers[gridX, gridY] = tower;
            }
        }
    }

    public void DestroyTower(int gridX, int gridY)
    {
        // If the grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0)
        {
            // If a tower is there, destroy it
            if (towers[gridX, gridY] != null)
            {
                Destroy(towers[gridX, gridY]);
            }
        }
    }
}
