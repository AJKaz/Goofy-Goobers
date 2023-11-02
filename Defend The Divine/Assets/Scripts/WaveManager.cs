using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField]
    private Transform[] enemySpawnPositions;

    /* TEMP VARIABLES FOR TEMP ENEMY SPAWNING */
    private float spawnDelay = 1f;
    private float spawnTimer = 0;

    private void Awake() {
        if (enemySpawnPositions.Length == 0) {
            Debug.LogError("Add enemy spawn positions to array");
        }
    }

    void Update()
    {
        // Temp spawn enemy every 5 seconds
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0;
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)].transform.position, Quaternion.identity);
            GameManager.Instance.enemies.Add(enemy.GetComponent<Enemy>());
        }
        
    }
}
