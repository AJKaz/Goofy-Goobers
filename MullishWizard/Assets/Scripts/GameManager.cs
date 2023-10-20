using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ThingSpawner spawner;

    [HideInInspector]
    public List<GameObject> enemies;
    // public List<Enemy> Enemies;

    [HideInInspector]
    public GameObject[,] towers;

    public PlayerMovement player;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        enemies = new List<GameObject>();
        towers = new GameObject[grid.width, grid.height];
    }

    public void CreateTower(GameObject towerPrefab, int gridX, int gridY) {
        // If grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0) {
            // If the tower prefab exists and there is not currently a tower there, create one
            if (towerPrefab != null && towers[gridX, gridY] == null) {
                GameObject tower = Instantiate(towerPrefab, grid.GetWorldPosition(gridX, gridY) + new Vector3(grid.cellSize / 2f, grid.cellSize / 2f), Quaternion.identity);
                tower.transform.SetParent(transform, true);
                towers[gridX, gridY] = tower;
            }
        }
    }

    public void DestroyTower(int gridX, int gridY) {
        // If the grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0) {
            // If a tower is there, destroy it
            if (towers[gridX, gridY] != null) {
                Destroy(towers[gridX, gridY]);
            }
        }
    }

    /*public bool RemoveTower(int x, int y) {
        return towers[x, y];
    }*/

    public bool RemoveEnemy(GameObject enemy) {
        return enemies.Remove(enemy);
    }

}
