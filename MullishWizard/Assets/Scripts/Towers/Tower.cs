using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float health = 10;
    private float range = 55;
    private float shootTimer = 0.5f;
    [SerializeField] GameObject projectilePrefab;
    //[SerializeField] GameObject spawner;

    public SimpleEnemySpawner spawner;

    public float Health { get { return health; } set { health = value; } }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //Debug.Log("thing: " + GetComponentInParent<SimpleEnemySpawner>().Enemies);
        List<GameObject> enemies = spawner.Enemies;
        GameObject closestEnemy = null;

        // Get closest enemy
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null || enemies[i].GetComponent<EnemyInfo>().IsDead) continue;
            if (Vector3.Distance(transform.position, enemies[i].transform.position) <= range)
            {
                if (closestEnemy == null) {
                    closestEnemy = enemies[i];
                } else {
                    if (Vector3.Distance(transform.position, enemies[i].transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position)) {
                        closestEnemy = enemies[i];
                    }
                }
            }
        }

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            shootTimer = 0.5f;
            if (closestEnemy != null) {
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                proj.GetComponent<Projectile>().targetPosition = closestEnemy.transform.position;
            }
        }
    }
}
