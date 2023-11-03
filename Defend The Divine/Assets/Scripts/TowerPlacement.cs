using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject towerGhost;

    private bool canPlaceTower = true;

    public enum TowerType {tower1, tower2 };
    public TowerType currentTowerType;
    [SerializeField] GameObject towerType1Prefab;
    [SerializeField] GameObject towerType2Prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameManager manager = GameManager.Instance;
        int gridX, gridY;
        manager.Grid.GetXY(manager.InputManager.MouseWorldPosition, out gridX, out gridY);

        // A tower may be placed when:
        //  * the ghost isn't colliding with a path
        //  * there isn't already a tower in the current spot
        canPlaceTower = 
            !towerGhost.GetComponent<TowerGhost>().CollidingWithPath
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
            switch (currentTowerType)
            {
                case TowerType.tower1:
                    towerPrefab = towerType1Prefab;
                    break;
                case TowerType.tower2:
                    towerPrefab = towerType2Prefab;
                    break;
                default:
                    towerType2Prefab = null;
                    break;
            }

            GameObject.Instantiate(towerPrefab, manager.Grid.GetTileCenter(gridX, gridY), Quaternion.identity);
            manager.Grid.SetValue(gridX, gridY, 1);
        }
    }
}
