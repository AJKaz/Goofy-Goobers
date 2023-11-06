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
            towerGhostSR.color = new Color(0f, 1f, 0f);
        }
        else
        {
            towerGhostSR.color = new Color(1f, 0f, 0f);
        }

        if (manager.inputManager.MouseLeftDownThisFrame && canPlaceTower)
        {
            GameObject.Instantiate(towerPrefab, manager.grid.GetTileCenter(gridX, gridY), Quaternion.identity);
            manager.grid.SetValue(gridX, gridY, 2);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }
}
