using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingTower : Tower
{
    [Header("Tower Specifics")]
    [SerializeField]
    protected float projectileSpeed = 15f;

    [SerializeField] protected int numEnemiesToDamage = 5;
    [SerializeField] private int numEnemiesToDamageUpgradeAmount = 2;

    protected override void Attack(Enemy target) {
        attackTimer = ATTACK_DELAY;
        GameObject obj = Instantiate(damagingPrefab, transform.position, Quaternion.identity);
        IceSpike iceSpike = obj.GetComponent<IceSpike>();
        iceSpike.SetDirection(target.transform.position);
        iceSpike.SetStats(damage, projectileSpeed, numEnemiesToDamage, this);
    }

    protected override Enemy GetTarget() {
        Enemy target = null;
        int highestWaypointIndex = -1;

        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) {
            if (GameManager.Instance.enemies[i] == null) continue;

            Vector3 offset = transform.position - GameManager.Instance.enemies[i].transform.position;
            if (offset.sqrMagnitude <= range * range) {
                int enemyWaypointIndex = GameManager.Instance.enemies[i].WaypointIndex;
                if (enemyWaypointIndex > highestWaypointIndex) {
                    highestWaypointIndex = enemyWaypointIndex;
                    target = GameManager.Instance.enemies[i];
                }
            }
        }
        return target;
    }

    public override void Upgrade() {
        if (upgradeLevel < maxUpgradeLevel && GameManager.Instance.Money >= upgradeCost) {
            GameManager.Instance.AddMoney(-upgradeCost);
            upgradeLevel++;

            damage += damageUpgradeAmount;
            range += rangeUpgradeAmount;
            ATTACK_DELAY -= attackSpeedUpgradeAmount;
            numEnemiesToDamage += numEnemiesToDamageUpgradeAmount;

            if (upgradeLevel % 2 == 0) sellPrice += (upgradeCost % 2 == 0) ? sellUpgradeIncrement : sellUpgradeIncrement + 1;
            else sellPrice += sellUpgradeIncrement;

            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);

            UpdateUpgradeButtonInteractibility();
            UpdatePopupUIText();
        }
    }
}
