using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private float health = 10;

    // This is the "cost" of ThingManager spawning this unit. ThingManager has a limited
    // budget for each wave, with more expensive units being more powerful.
    [SerializeField]
    private short spawnPoints = 10;

    private bool isDead = false;

    public float Health { get { return health; } set { health = value; } }
    public short SpawnPoints { get { return spawnPoints; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }


    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
