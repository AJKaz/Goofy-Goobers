using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [SerializeField] private TowerPlacement.TowerType towerType = TowerPlacement.TowerType.tower1;

    public void OnClick()
    {
        switch (towerType)
        {
            case TowerPlacement.TowerType.tower1:
                GameManager.Instance.towerPlacement.SetCurrentTowerType(TowerPlacement.TowerType.tower1);
                break;
            case TowerPlacement.TowerType.tower2:
                GameManager.Instance.towerPlacement.SetCurrentTowerType(TowerPlacement.TowerType.tower2);
                break;
            case TowerPlacement.TowerType.tower3:
                GameManager.Instance.towerPlacement.SetCurrentTowerType(TowerPlacement.TowerType.tower3);
                break;
            default:
                break;
        }
    }
}