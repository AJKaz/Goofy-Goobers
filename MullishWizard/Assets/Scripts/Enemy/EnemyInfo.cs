using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : Entity
{

    // This is the "cost" of ThingManager spawning this unit. ThingManager has a limited
    // budget for each wave, with more expensive units being more powerful.
    [SerializeField]
    private short spawnPoints = 10;

    public short SpawnPoints { get { return spawnPoints; } }

    override protected void Die() {
        isDead = true;
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(gameObject);
    }
}
