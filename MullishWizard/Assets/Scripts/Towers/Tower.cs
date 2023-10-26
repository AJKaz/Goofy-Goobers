using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    [SerializeField]
    protected float damage = 5f;

    [SerializeField]
    protected float speed = 15f;

    [SerializeField]
    protected float range = 5;

    [SerializeField]
    protected float SHOOT_DELAY = 0.5f;

    protected float shootTimer;

    [SerializeField] 
    protected GameObject projectilePrefab;

    private int x, y;

    private void Awake() {
        shootTimer = SHOOT_DELAY;
    }

    protected virtual void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot();
        }
    }

    protected override void Die() {
        //GameManager.Instance.RemoveTower(x, y);
        Destroy(gameObject);
    }

    protected virtual void Shoot() {
        GameObject target = GetNearestTarget();
        shootTimer = SHOOT_DELAY;
        if (target != null) {
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.SetTarget(target.transform.position);
            projectile.SetStats(damage, speed);
        }
    }

    protected virtual GameObject GetNearestTarget() {
        GameObject closestEnemy = null;
        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) {
            if (GameManager.Instance.enemies[i] == null || GameManager.Instance.enemies[i].GetComponent<EnemyInfo>().IsDead) continue;
            if (Vector3.Distance(transform.position, GameManager.Instance.enemies[i].transform.position) <= range) {
                if (closestEnemy == null) {
                    closestEnemy = GameManager.Instance.enemies[i];
                }
                else {
                    if (Vector3.Distance(transform.position, GameManager.Instance.enemies[i].transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position)) {
                        closestEnemy = GameManager.Instance.enemies[i];
                    }
                }
            }
        }
        return closestEnemy;
    }
}
