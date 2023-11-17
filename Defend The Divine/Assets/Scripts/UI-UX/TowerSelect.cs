using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{

    [SerializeField] private TowerPlacement.TowerType towerType = TowerPlacement.TowerType.tower1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        switch (towerType)
        {
            case TowerPlacement.TowerType.tower1:
                GameManager.Instance.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.tower1;
                break;
            case TowerPlacement.TowerType.tower2:
                GameManager.Instance.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.tower2;
                break;
            default:
                break;
        }
    }
}