using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // TODO: Make array
    public GameObject enemyPrefab;

    [SerializeField]
    private Transform[] enemySpawnPositions;
    public Transform[] EnemySpawnPositions { get { return enemySpawnPositions; } }


    /* TEMP VARIABLES FOR TEMP ENEMY SPAWNING */
    //private float spawnDelay = 1f;
    //private float spawnTimer = 0;

    private void Awake() {
        if (enemySpawnPositions.Length == 0) {
            Debug.LogError("Add enemy spawn positions to array");
        }
        SpawnEnemyGroups(enemyPrefab, 5, 5000, 10);
        SpawnEnemyGroups(enemyPrefab, 5, 3000, 10);
    }

    void Update()
    {
/*        // Temp spawn enemy every 5 seconds
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0;
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)].transform.position, Quaternion.identity);
            GameManager.Instance.enemies.Add(enemy.GetComponent<Enemy>());
        }*/
        
        // TODO: IMPLEMENT AND EXPAND
/*        switch (currentWave)
        {
            case 1:
                SpawnEnemyGroups(enemyPrefab, 1, 3000, 8);
                break;
            case 2:
                SpawnEnemyGroups(enemyPrefab, 1, 1500, 16);
                break;
            default:
                Debug.LogException("Wave " + currentWave + " has not been made/configured!");
                break;
        }*/
    }

    public void SpawnEnemyGroups(
        GameObject enemyType, 
        int groupSize, 
        int msBetweenGroups, 
        int numberOfGroups)
    {
        // gameObject is autocreated by unity, and references the object this
        // script is attached to
        gameObject.AddComponent<GroupSpawner>().Initialize(
        enemyType, 
        groupSize, 
        msBetweenGroups, 
        numberOfGroups);
;    }
}
