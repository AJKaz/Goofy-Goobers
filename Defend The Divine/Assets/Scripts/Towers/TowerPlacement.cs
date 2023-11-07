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
    private TowerGhost towerGhost;

    private bool canPlaceTower = true;

    void Update()
    {
        GameManager manager = GameManager.Instance;

        /* A tower may be placed when:
         *   player has enough money
         *   the ghost isn't colliding with a path
         *   there isn't already a tower in the current spot
         *   mouse isn't over menu HUD
         */
        canPlaceTower =
            manager.Money >= towerPrefab.Cost
            && !towerGhost.CollidingWithPath
            && !manager.GetComponent<MouseUICheck>().IsPointerOverUIElement();

        Vector2 currentMousePosition = manager.inputManager.MouseWorldPosition;
        towerGhost.transform.position = currentMousePosition;

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
            Instantiate(towerPrefab, currentMousePosition, Quaternion.identity);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }
}
