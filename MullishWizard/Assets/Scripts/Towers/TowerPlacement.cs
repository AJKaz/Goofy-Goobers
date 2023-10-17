using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Grid), typeof(GameManager))]
public class TowerPlacement : MonoBehaviour {
    public enum TowerType { regTower, wall };
    [SerializeField] private Grid grid;
    //[SerializeField] private TowerManager towerManager;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private PlayerInventory playerInventory;   // should use GameManager playerInventory in future (it doens't exist yet)
    private GameObject towerTobuild;
    public TowerType currentTowerType;

    //public TowerType CurrentTowerType { get { return currentTowerType; } }

    // Building mode switch and tower to place
    private bool isBuilding = false;
    private GameObject currentTower;
    public bool IsBuilding { get { return isBuilding; } }

    // 1 scrap, 1 wood
    [SerializeField] private int[] towerResourceCost = { 1, 1 };

    private bool canAffordTower = false;

    void Update() {
        if (PauseControl.isPaused) return;

        // Check if building mode is being activated
        if (Keyboard.current.bKey.wasPressedThisFrame) {
            Debug.Log("b");
            isBuilding = !isBuilding;
        }

        // Does the player have enough resources?
        canAffordTower =
            playerInventory.GetResourceQuantity(ResourceType.Scrap) >= towerResourceCost[0]
            && playerInventory.GetResourceQuantity(ResourceType.Wood) >= towerResourceCost[1];

        DebugCanvas.AddDebugText("Tower Cost", $"{towerResourceCost[0]} scrap, {towerResourceCost[1]} wood");
        DebugCanvas.AddDebugText("Can Afford Tower", $"{canAffordTower}");

        // left click places a tower
        if (Mouse.current.leftButton.wasPressedThisFrame && isBuilding && canAffordTower) {
            switch (currentTowerType) {
                case TowerType.regTower:
                    towerTobuild = towerPrefab;
                    break;
                case TowerType.wall:
                    // Add wall Reference and set towerToBuild to be the wall
                    break;
                default:
                    towerTobuild = null;
                    break;
            }
            if (canAffordTower && towerTobuild != null) {
                // Get mouse position
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                int gridX, gridY;
                // Get grid position
                grid.GetXY(worldMousePosition, out gridX, out gridY);

                // Create tower
                GameManager.Instance.CreateTower(towerTobuild, gridX, gridY);

                playerInventory.RemoveResources(ResourceType.Scrap, towerResourceCost[0]);
                playerInventory.RemoveResources(ResourceType.Wood, towerResourceCost[1]);
            }
            // right click destroys a tower
            else if (Mouse.current.rightButton.wasPressedThisFrame && isBuilding) {
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                int gridX, gridY;
                grid.GetXY(worldMousePosition, out gridX, out gridY);
                GameManager.Instance.DestroyTower(gridX, gridY);
            }
        }
    }
}
