using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [SerializeField] private GameObject towerPlacement;
    private string towerID;
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
        towerID = GetComponentInChildren<TextMeshProUGUI>().text;
        switch (towerID)
        {
            case "tower1":
                towerPlacement.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.regTower;
                break;
            case "tower2":
                towerPlacement.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.wall;
                break;
            default:
                break;
        }
    }
}
