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
        int gridX, gridY;
        manager.Grid.GetXY(manager.InputManager.MouseWorldPosition, out gridX, out gridY);

        /* A tower may be placed when:
         *   player has enough money
         *   the ghost isn't colliding with a path
         *   there isn't already a tower in the current spot
         *   mouse isn't over menu HUD
         */
        canPlaceTower =
            GameManager.Instance.Money >= towerPrefab.Cost
            && !towerGhost.GetComponent<TowerGhost>().CollidingWithPath
            && manager.Grid.GetValue(gridX, gridY) != 1
            && !manager.GetComponent<MouseUICheck>().IsPointerOverUIElement();

        towerGhost.transform.position = manager.Grid.GetTileCenter(gridX, gridY);

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

        if (manager.InputManager.MouseLeftDownThisFrame && canPlaceTower)
        {
            GameObject.Instantiate(towerPrefab, manager.Grid.GetTileCenter(gridX, gridY), Quaternion.identity);
            manager.Grid.SetValue(gridX, gridY, 1);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }
}
