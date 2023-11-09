using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    private Tower towerPrefab;
    [SerializeField]
    private GameObject towerGhost;

    private bool canPlaceTower = true;

    public enum TowerType {tower1, tower2 };
    public TowerType currentTowerType;
    [SerializeField] Tower towerType1Prefab;
    [SerializeField] Tower towerType2Prefab;

    void Update()
    {
        GameManager manager = GameManager.Instance;

        // If the player's mouse isn't in a place that allows placement (the grid),
        // don't bother.
        if (!manager.grid.PositionInGrid(manager.inputManager.MouseWorldPosition))
        {
            return;
        }

        int gridX, gridY;
        manager.grid.GetXY(manager.inputManager.MouseWorldPosition, out gridX, out gridY);

        /* A tower may be placed when:
         *   player has enough money
         *   the ghost isn't colliding with a path
         *   there isn't already a tower in the current spot
         *   mouse isn't over menu HUD
         */
        canPlaceTower =
            GameManager.Instance.Money >= towerPrefab.Cost
            && manager.grid.GetValue(gridX, gridY) == 0
            && !manager.GetComponent<MouseUICheck>().IsPointerOverUIElement();

        towerGhost.transform.position = manager.grid.GetTileCenter(gridX, gridY);

        // Show when a tower can be placed
        SpriteRenderer towerGhostSR = towerGhost.GetComponent<SpriteRenderer>();
        if (canPlaceTower)
        {
            towerGhostSR.enabled = true;
        }
        else
        {
            towerGhostSR.enabled = false;
        }

        if (manager.inputManager.MouseLeftDownThisFrame && canPlaceTower)
        {
            switch (currentTowerType)
            {
                case TowerType.tower1:
                    towerPrefab = towerType1Prefab;
                    break;
                case TowerType.tower2:
                    towerPrefab = towerType2Prefab;
                    break;
                default:
                    break;
            }

            GameObject.Instantiate(towerPrefab, manager.grid.GetTileCenter(gridX, gridY), Quaternion.identity);
            manager.grid.SetValue(gridX, gridY, 2);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }
}
