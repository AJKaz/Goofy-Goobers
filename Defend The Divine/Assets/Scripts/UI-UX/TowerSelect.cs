using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
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
        string towerID = GetComponentInChildren<TextMeshProUGUI>().text;
        switch (towerID)
        {
            case "Tower1":
                GameManager.Instance.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.tower1;
                break;
            case "Tower2":
                GameManager.Instance.GetComponent<TowerPlacement>().currentTowerType = TowerPlacement.TowerType.tower2;
                break;
            default:
                break;
        }
    }
}