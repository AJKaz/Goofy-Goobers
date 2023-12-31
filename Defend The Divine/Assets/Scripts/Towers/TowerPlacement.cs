using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("Ghost Towers")]
    [SerializeField] private TowerGhost tower1Ghost;
    [SerializeField] private TowerGhost tower2Ghost;
    [SerializeField] private TowerGhost tower3Ghost;

    public enum TowerType {tower1, tower2, tower3 };
    [Header("Towers")]
    public Tower towerType1Prefab;
    public Tower towerType2Prefab;
    public Tower towerType3Prefab;

    private Tower currentTowerPrefab = null;
    private TowerGhost currentGhostTower;
    private bool canPlaceTower = true;

    void Update()
    {
        if (currentTowerPrefab == null || currentGhostTower == null) return;

        /* A tower may be placed when:
         *   player has enough money
         *   the ghost isn't colliding with a path
         *   there isn't already a tower in the current spot
         *   mouse isn't over menu HUD
         */
        canPlaceTower =
            GameManager.Instance.Money >= currentTowerPrefab.Cost
            && !currentGhostTower.CollidingWithPath
            && !GameManager.Instance.GetComponent<MouseUICheck>().IsPointerOverUIElement();

        Vector2 currentMousePosition = GameManager.Instance.inputManager.MouseWorldPosition;
        currentGhostTower.transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, 0);

        // Show when a tower can be placed
        SpriteRenderer towerGhostSR = currentGhostTower.GetComponent<SpriteRenderer>();
        if (!canPlaceTower)
        {
            towerGhostSR.color = new Color(0.8f, 0f, 0f, 0.75f);
        }
        else {
            towerGhostSR.color = new Color(1f, 1f, 1f, 0.75f);
        }

        if (GameManager.Instance.inputManager.MouseLeftDownThisFrame && canPlaceTower)
        {
            AudioManager.instance.PlaySound("TowerPlaced");

            Instantiate(currentTowerPrefab, currentGhostTower.transform.position, Quaternion.identity);
            GameManager.Instance.AddMoney(-currentTowerPrefab.Cost);
            currentTowerPrefab = null;
            deselectTower();
        }
        else if (GameManager.Instance.inputManager.MouseRightDownThisFrame && currentGhostTower != null) {
            deselectTower();
        }
    }

    public void SetCurrentTowerType(TowerType towerType) {
        switch (towerType) {
            case TowerType.tower1:
                deselectTower();
                currentTowerPrefab = towerType1Prefab;
                currentGhostTower = tower1Ghost;
                break;
            case TowerType.tower2:
                deselectTower();
                currentTowerPrefab = towerType2Prefab;
                currentGhostTower = tower2Ghost;
                break;
            case TowerType.tower3:
                deselectTower();
                currentTowerPrefab = towerType3Prefab;
                currentGhostTower = tower3Ghost;
                break;
            default:
                break;
        }
    }

    public void deselectTower() {
        if (currentGhostTower == null) return;
        currentGhostTower.transform.position = new Vector2(0f, 255f);
        currentGhostTower = null;
    }
}
