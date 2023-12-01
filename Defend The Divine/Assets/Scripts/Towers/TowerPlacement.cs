using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    private Tower towerPrefab;

    [Header("Ghost Towers")]
    [SerializeField] private TowerGhost tower1Ghost;
    [SerializeField] private TowerGhost tower2Ghost;
    [SerializeField] private TowerGhost tower3Ghost;
    private TowerGhost currentTowerGhost;
    private bool canPlaceTower = true;

    public enum TowerType {tower1, tower2, tower3 };
    [Header("Towers")]
    [SerializeField] Tower towerType1Prefab;
    [SerializeField] Tower towerType2Prefab;
    [SerializeField] Tower towerType3Prefab;

    private void Awake() {
        /* REMOVE THIS ONCE ONE CLICK PER TOWER PLACEMENT IS IMPLEMENTED */
        currentTowerGhost = tower1Ghost;
    }

    void Update()
    {
        /* A tower may be placed when:
         *   player has enough money
         *   the ghost isn't colliding with a path
         *   there isn't already a tower in the current spot
         *   mouse isn't over menu HUD
         */
        canPlaceTower =
            GameManager.Instance.Money >= towerPrefab.Cost
            && !currentTowerGhost.CollidingWithPath
            && !GameManager.Instance.GetComponent<MouseUICheck>().IsPointerOverUIElement();

        Vector2 currentMousePosition = GameManager.Instance.inputManager.MouseWorldPosition;
        currentTowerGhost.transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, 0);

        // Show when a tower can be placed
        SpriteRenderer towerGhostSR = currentTowerGhost.GetComponent<SpriteRenderer>();
        if (!canPlaceTower)
        {
            towerGhostSR.color = new Color(0.8f, 0f, 0f, 0.8f);
        }
        else {
            towerGhostSR.color = new Color(1f, 1f, 1f, 0.8f);
        }

        if (GameManager.Instance.inputManager.MouseLeftDownThisFrame && canPlaceTower)
        {
            GameObject.Instantiate(towerPrefab, currentTowerGhost.transform.position, Quaternion.identity);
            GameManager.Instance.AddMoney(-towerPrefab.Cost);
        }
    }

    public void SetCurrentTowerType(TowerType towerType) {
        switch (towerType) {
            case TowerType.tower1:
                currentTowerGhost.transform.position = new Vector2(-9f, 255f);
                towerPrefab = towerType1Prefab;
                currentTowerGhost = tower1Ghost;
                break;
            case TowerType.tower2:
                currentTowerGhost.transform.position = new Vector2(0f, 255f);
                towerPrefab = towerType2Prefab;
                currentTowerGhost = tower2Ghost;
                break;
            case TowerType.tower3:
                currentTowerGhost.transform.position = new Vector2(9f, 255f);
                towerPrefab = towerType3Prefab;
                currentTowerGhost = tower3Ghost;
                break;
            default:
                break;
        }
    }
}
