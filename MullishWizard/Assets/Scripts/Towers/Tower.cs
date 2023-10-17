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
    private const float SHOOT_DELAY = 0.5f;

    private float shootTimer;

    [SerializeField] 
    GameObject projectilePrefab;
    //[SerializeField] GameObject spawner;

    public SimpleEnemySpawner spawner;

    public float Health { get { return health; } set { health = value; } }

    private void Awake() {
        shootTimer = SHOOT_DELAY;
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            TowerDestroyed();
        }

        GameObject target = GetTarget();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Shoot(target);
        }
    }

    protected virtual void TowerDestroyed() {
        GameManager.Instance.RemoveTower(this);
        Destroy(gameObject);
    }

    protected virtual void Shoot(GameObject target) {
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
