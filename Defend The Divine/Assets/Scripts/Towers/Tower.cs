using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected float damage = 5f;

    [SerializeField]
    protected float projectileSpeed = 15f;

    [SerializeField]
    protected float range = 5f;

    /* Use as a const */
    [SerializeField]
    protected float SHOOT_DELAY = 0.5f;

    protected float shootTimer = 0.05f;

    [SerializeField]
    protected int cost = 10;

    [SerializeField]
    protected GameObject projectilePrefab;

    public int Cost { get { return cost; } }

    protected virtual void Update() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot();
        }
    }

    protected virtual void Shoot() {
        Enemy target = GetTarget();
        shootTimer = SHOOT_DELAY;
        if (target != null) {
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.SetTarget(target.transform.position);
            projectile.SetStats(damage, projectileSpeed);
        }
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

}
