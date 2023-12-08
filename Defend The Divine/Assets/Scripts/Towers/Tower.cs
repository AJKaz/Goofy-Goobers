using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected float damage = 5f;
    [SerializeField] protected float range = 5f;

    /* Use as a const, only to be changed in UpgradeAttackSpeed */
    [SerializeField] protected float ATTACK_DELAY = 0.5f;

    protected float attackTimer = 0.05f;

    [SerializeField] protected int cost = 10;

    [SerializeField] protected GameObject damagingPrefab;

    protected bool hasCreatedUI = false;
    protected bool mouseIsOver = false;
    protected GameObject createdUi;
    protected GameObject visibleRange;

    [Header("UI Stuff")]
    [SerializeField] protected GameObject uiPopupPrefab;
    [SerializeField] protected GameObject rangePrefab;

    [Header("Upgrades")]
    [SerializeField] protected int upgradeCost = 3;
    [SerializeField] protected int maxUpgradeLevel = 4;
    [SerializeField] protected float damageUpgradeAmount = 0.5f;
    [SerializeField] protected float rangeUpgradeAmount = 0.5f;
    [SerializeField] protected float attackSpeedUpgradeAmount = 0.05f;

    /* Current Upgrade Level*/
    protected int upgradeLevel = 1;

    protected int targetingMode = 0;

    protected int sellPrice = 0;
    protected int sellUpgradeIncrement = 0;
    protected int enemiesKilled = 0;

    protected HandlePanelVisibility createdUiReference;

    public bool isPopupUIActive = false;

    public int Cost { get { return cost; } }
    
    protected void Awake() {
        sellPrice = cost / 2;
        sellUpgradeIncrement = upgradeCost / 2;
    }

    protected virtual void Update() {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) {
            Enemy target = GetTarget();
            if (target != null) {
                Attack(target);
            }
        }
        if (isPopupUIActive) {
            UpdateUpgradeButtonInteractibility();
        }
    }

    protected abstract void Attack(Enemy target);

    protected abstract Enemy GetTarget();


    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasCreatedUI)
        {
            // Create a ui element on top of the tower
            createdUi = GameObject.Instantiate(uiPopupPrefab, transform.position, Quaternion.identity, GameManager.Instance.towerUiCanvas.transform);
            createdUi.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);

            createdUiReference = createdUi.GetComponent<HandlePanelVisibility>();
            createdUiReference.tower = this;

            // Use to calculate position relative to screen
            Vector2 screenExtents = new Vector2(Camera.main.orthographicSize * Screen.width / (float)Screen.height, Camera.main.orthographicSize);

            // Check which side of the screen the tower is on and move the UI accordingly
            createdUi.transform.localPosition = new Vector3(245, -170, 0);

            // Calculate if the ui is clipping out of the vertical camera view
            //float topOfUi = Camera.main.WorldToScreenPoint(transform.position).y + createdUi.GetComponent<RectTransform>().rect.height / 2;
            //if (topOfUi > Camera.main.WorldToScreenPoint(screenExtents).y)
            //{
            //    float difference = Camera.main.WorldToScreenPoint(screenExtents).y - topOfUi;
            //    createdUi.transform.position += new Vector3(0, difference - 10, 0);
            //}
            //
            //float bottomOfUi = Camera.main.WorldToScreenPoint(transform.position).y - createdUi.GetComponent<RectTransform>().rect.height / 2;
            //if (bottomOfUi < 0)
            //{
            //    float difference = 0 - bottomOfUi;
            //    createdUi.transform.position += new Vector3(0, difference + 10, 0);
            //}

            visibleRange = GameObject.Instantiate(rangePrefab, transform.position, Quaternion.identity);
            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);

            createdUiReference.visibleRange = visibleRange;

            hasCreatedUI = true;
            UpdatePopupUIText();
        }
        createdUi.SetActive(true);
        isPopupUIActive = true;
        visibleRange.SetActive(true);
    }

    // Will be overridden in Sword tower, as it only has 1 possible way of attacking
    public virtual void CycleTargetingMode()
    {
        // Doesnt work if ++ is placed after the variable
        targetingMode = ++targetingMode % 2;
        UpdatePopupUIText();
    }

    public virtual void Upgrade() {
        if (upgradeLevel < maxUpgradeLevel && GameManager.Instance.Money >= upgradeCost) {
            GameManager.Instance.AddMoney(-upgradeCost);
            upgradeLevel++;

            damage += damageUpgradeAmount;
            range += rangeUpgradeAmount;
            ATTACK_DELAY -= attackSpeedUpgradeAmount;

            if (upgradeLevel % 2 == 0) sellPrice += (upgradeCost % 2 == 0) ? sellUpgradeIncrement : sellUpgradeIncrement + 1;
            else sellPrice += sellUpgradeIncrement;
            

            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);

            UpdateUpgradeButtonInteractibility();
            UpdatePopupUIText();
        }
    }

    public virtual void Sell(GameObject visibleRange, GameObject popupUI) {
        GameManager.Instance.AddMoney(sellPrice);
        Destroy(gameObject);

        if (visibleRange) Destroy(visibleRange);
        if (popupUI) Destroy(popupUI);
    }

    public void KilledEnemy() {
        enemiesKilled++;
        UpdatePopupUIText();
    }

    protected void UpdateUpgradeButtonInteractibility() {
        if (upgradeLevel == maxUpgradeLevel) {
            createdUiReference.upgradeButton.interactable = false;
            return;
        }

        if (GameManager.Instance.Money < upgradeCost) {
            createdUiReference.upgradeButton.interactable = false;
        }
        else {
            createdUiReference.upgradeButton.interactable = true;
        }
    }

    // Will be overridden in Sword tower, as it only has 1 possible way of attacking
    protected virtual string ParseTargetingMode()
    {
        switch (targetingMode)
        {
            case 0:
                return "First";
            case 1:
                return "Last";
            default:
                Debug.LogError("targetingMode value out of bounds (" + targetingMode + ")");
                return "targetingMode value out of bounds (" + targetingMode + ")";
        }
    }

    protected void UpdatePopupUIText() {
        if (createdUiReference == null) return;

        if (createdUiReference.infoText != null) {
            createdUiReference.infoText.text =
            $"Demons Killed: {enemiesKilled}\n" +
            $"Damage: {damage}\n" +
            $"Range: {range}\n" +
            $"Level: {upgradeLevel} / {maxUpgradeLevel}\n" +
            $"Targeting: {ParseTargetingMode()}\n";
        }
        if (createdUiReference.upgradeButtonText != null) {
            createdUiReference.upgradeButtonText.text = $"{upgradeCost}";
        }
        if (createdUiReference.sellButtonText != null) {
            createdUiReference.sellButtonText.text = $"+{sellPrice}";
        }
        
    }
}
