using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected float damage = 5f;

    [SerializeField]
    protected float projectileSpeed = 15f;

    [SerializeField]
    protected float range = 5f;

    /* Use as a const, only to be changed in UpgradeAttackSpeed */
    [SerializeField]
    protected float ATTACK_DELAY = 0.5f;

    protected float shootTimer = 0.05f;

    [SerializeField]
    protected int cost = 10;

    [SerializeField]
    protected GameObject damagingPrefab;

    protected bool hasCreatedUI = false;
    protected bool mouseIsOver = false;
    protected GameObject createdUi;
    protected GameObject visibleRange;

    [SerializeField]
    protected GameObject uiPopupPrefab;

    [SerializeField]
    protected GameObject rangePrefab;
    
    [Header("Upgrade Amounts")]
    [SerializeField] protected float damageUpgradeAmount = 0.5f;
    [SerializeField] protected int damageUpgradeCost = 3;
    [SerializeField] protected int maxDamageUpgradeLevel = 8;
    [SerializeField] protected float rangeUpgradeAmount = 0.5f;
    [SerializeField] protected int rangeUpgradeCost = 3;
    [SerializeField] protected int maxRangeUpgradeLevel = 8;
    [SerializeField] protected float attackSpeedUpgradeAmount = 0.05f;
    [SerializeField] protected int attackSpeedUpgradeCost = 3;
    [SerializeField] protected int maxAttackSpeedUpgradeLevel = 8;

    /* Current Upgrade Level*/
    protected int damageLevel = 1;
    protected int rangeLevel = 1;
    protected int attackSpeedLevel = 1;

    public int Cost { get { return cost; } }

    protected virtual void Update() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Enemy target = GetTarget();
            if (target != null) {
                Attack(target);
            }
        }
    }

    protected virtual void Attack(Enemy target) {
        shootTimer = ATTACK_DELAY;
        GameObject proj = Instantiate(damagingPrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.SetTarget(target.transform.position);
        projectile.SetStats(damage, projectileSpeed);
    }

    protected virtual Enemy GetTarget() {
        Enemy closestEnemy = null;
        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) {
            if (GameManager.Instance.enemies[i] == null) continue;
            Vector3 offset = transform.position - GameManager.Instance.enemies[i].transform.position;
            if (offset.sqrMagnitude <= range * range) {
                if (closestEnemy == null) {
                    closestEnemy = GameManager.Instance.enemies[i];
                }
                else {
                    Vector3 offsetToCurrentEnemy = transform.position - GameManager.Instance.enemies[i].transform.position;
                    Vector3 offsetToClosestEnemy = transform.position - closestEnemy.transform.position;
                    if (offsetToCurrentEnemy.sqrMagnitude < offsetToClosestEnemy.sqrMagnitude) {
                        closestEnemy = GameManager.Instance.enemies[i];
                    }
                }
            }
        }
        return closestEnemy;
    }


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

            createdUi.GetComponent<HandlePanelVisibility>().tower = this;
            
            // Use to calculate position relative to screen
            Vector2 screenExtents = new Vector2(Camera.main.orthographicSize * Screen.width / (float)Screen.height, Camera.main.orthographicSize);

            // Check which side of the screen the tower is on and move the UI accordingly
            if (transform.position.x <= 0)
            {
                createdUi.transform.localPosition = new Vector3(250, -113, 0);
            }
            else
            {
                createdUi.transform.localPosition = new Vector3(-250, -113, 0);
            }

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

            createdUi.GetComponent<HandlePanelVisibility>().visibleRange = visibleRange;

            hasCreatedUI = true;
        }
        createdUi.SetActive(true);
        visibleRange.SetActive(true);
    }

    public void UpgradeDamage() {
        Debug.Log("DMG UPG");
        if (damageLevel <= maxDamageUpgradeLevel && GameManager.Instance.Money >= damageUpgradeCost) {
            damage += damageUpgradeAmount;
            damageLevel++;
            GameManager.Instance.AddMoney(-damageUpgradeCost);
        }
    }

    public void UpgradeRange() {
        Debug.Log("RNG UPG");
        if (rangeLevel <= maxRangeUpgradeLevel && GameManager.Instance.Money >= rangeUpgradeCost) {
            range += rangeUpgradeAmount;
            rangeLevel++;
            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);
            GameManager.Instance.AddMoney(-rangeUpgradeCost);
        }
    }

    public void UpgradeAttackSpeed() {
        Debug.Log("ATK SPD UPG");
        if (attackSpeedLevel <= maxAttackSpeedUpgradeLevel && GameManager.Instance.Money >= attackSpeedUpgradeCost) {
            ATTACK_DELAY -= attackSpeedUpgradeAmount;
            attackSpeedLevel++;
            GameManager.Instance.AddMoney(-attackSpeedUpgradeCost);
        }
    }

}
