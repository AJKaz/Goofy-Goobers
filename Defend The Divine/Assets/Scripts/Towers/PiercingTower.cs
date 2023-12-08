using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingTower : Tower
{
    [Header("Tower Specifics")]
    [SerializeField]
    protected float projectileSpeed = 15f;

    protected override void Attack(Enemy target) {
        attackTimer = ATTACK_DELAY;
        GameObject obj = Instantiate(damagingPrefab, transform.position, Quaternion.identity);
        IceSpike iceSpike = obj.GetComponent<IceSpike>();
        iceSpike.SetDirection(target.transform.position);
        iceSpike.SetStats(damage, projectileSpeed, this);
    }

    protected override Enemy GetTarget() {
        Enemy target = null;
        int currentBestTargetIndex;
        if (targetingMode == 0) { currentBestTargetIndex = -1; }
        else { currentBestTargetIndex = 9999; }

        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) {
            if (GameManager.Instance.enemies[i] == null) continue;

            Vector3 offset = transform.position - GameManager.Instance.enemies[i].transform.position;
            if (offset.sqrMagnitude <= range * range) {
                int enemyWaypointIndex = GameManager.Instance.enemies[i].WaypointIndex;
                // First (closest to pillar) enemy in range
                if (enemyWaypointIndex > currentBestTargetIndex && targetingMode == 0) {
                    currentBestTargetIndex = enemyWaypointIndex;
                    target = GameManager.Instance.enemies[i];
                } // Last (farthest from pillar) enemy in range
                else if (enemyWaypointIndex < currentBestTargetIndex && targetingMode == 1) {
                    currentBestTargetIndex = enemyWaypointIndex;
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

            if (upgradeLevel % 2 == 0) sellPrice += (upgradeCost % 2 == 0) ? sellUpgradeIncrement : sellUpgradeIncrement + 1;
            else sellPrice += sellUpgradeIncrement;

            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);

            UpdateUpgradeButtonInteractibility();
            UpdatePopupUIText();
        }
    }
}
