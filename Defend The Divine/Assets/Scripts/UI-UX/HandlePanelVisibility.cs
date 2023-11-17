using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandlePanelVisibility : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseIsOver = false;
    [HideInInspector] public GameObject visibleRange;
    [HideInInspector] public Tower tower;
    [SerializeField] Button upgradeDamageButton;
    [SerializeField] Button upgradeRangeButton;
    [SerializeField] Button upgradeSpeedButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void Start()
    {
        upgradeDamageButton.onClick.AddListener(delegate { tower.UpgradeDamage(); });
        upgradeRangeButton.onClick.AddListener(delegate { tower.UpgradeRange(); });
        upgradeSpeedButton.onClick.AddListener(delegate { tower.UpgradeAttackSpeed(); });
    }


    public void OnDeselect(BaseEventData eventData)
    {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver)
        {
            gameObject.SetActive(false);
            visibleRange.SetActive(false);
        }
            
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}