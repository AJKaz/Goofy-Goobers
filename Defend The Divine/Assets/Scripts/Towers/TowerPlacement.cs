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

    public enum TowerType {tower1, tower2 };
    public TowerType currentTowerType;
    [SerializeField] Tower towerType1Prefab;
    [SerializeField] Tower towerType2Prefab;

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
        towerGhost.transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, 0);

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

            GameObject.Instantiate(towerPrefab, towerGhost.transform.position, Quaternion.identity);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }
}
