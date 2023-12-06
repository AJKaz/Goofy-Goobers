using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HandlePanelVisibility : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseIsOver = false;
    [HideInInspector] public GameObject visibleRange;
    [HideInInspector] public Tower tower;
    public Button upgradeButton;
    public Button sellButton;
    public TMP_Text upgradeButtonText;
    public TMP_Text sellButtonText;
    public TMP_Text infoText;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        GameManager.Instance.towerPlacement.deselectTower();
    }

    private void Start()
    {
        upgradeButton.onClick.AddListener(delegate { tower.Upgrade(); });
        sellButton.onClick.AddListener(delegate { tower.Sell(visibleRange, gameObject); });
    }


    public void OnDeselect(BaseEventData eventData)
    {
        //Close the Window on Deselect only if a click occurred outside this panel
        if (!mouseIsOver)
        {
            if (gameObject) gameObject.SetActive(false);
            if (visibleRange) visibleRange.SetActive(false);
            if (tower) tower.isPopupUIActive = false;
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