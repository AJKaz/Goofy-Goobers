using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float health = 10;

    [SerializeField]
    private float range = 5;

    [SerializeField]
    private float SHOOT_DELAY = 0.5f;

    private float shootTimer;

    [SerializeField] 
    GameObject projectilePrefab;

    private int x, y;

    public float Health { get { return health; } set { health = value; } }

    private void Awake() {
        shootTimer = SHOOT_DELAY;
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            TowerDestroyed();
        }

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot();
        }
    }

    protected virtual void TowerDestroyed() {
        //GameManager.Instance.RemoveTower(x, y);
        Destroy(gameObject);
    }

    protected virtual void Shoot() {
        GameObject target = GetTarget();
        shootTimer = SHOOT_DELAY;
        if (target != null) {
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().targetPosition = target.transform.position;
        }
    }

    protected virtual GameObject GetTarget() {
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
