using UnityEngine;

public class CannonTower : Tower
{
    [Header("Tower Specifics")]
    [SerializeField]
    protected float projectileSpeed = 15f;

    protected override void Attack(Enemy target) {
        AudioManager.instance.PlaySound("CannonTower");

        attackTimer = ATTACK_DELAY;
        GameObject proj = Instantiate(damagingPrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.SetTarget(target.transform.position);
        projectile.SetStats(damage, projectileSpeed, this);
    }

    // Finds closest enemy to tower
    /*protected override Enemy GetTarget() {
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
    }*/

    /// <summary>
    /// Finds closest enemy to DivinePillar
    /// </summary>
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

}
