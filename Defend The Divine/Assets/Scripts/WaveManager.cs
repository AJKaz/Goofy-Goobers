using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Can be made into an array if necessary
    public GameObject enemyPrefab;

    [SerializeField]
    private Transform[] enemySpawnPositions;
    public Transform[] EnemySpawnPositions { get { return enemySpawnPositions; } }
    
    [SerializeField]
    private int currentWave;
    private float waveTimestamp;

    private void Awake() {
        if (enemySpawnPositions.Length == 0) {
            Debug.LogError("Enemy spawn positions array is empty in WaveManager");
        }
        currentWave = 0;
    }

    void Update()
    {
        if (GameManager.Instance.enemies.Count < 1 &&
            Time.realtimeSinceStartup > 5 + waveTimestamp)
        {
            currentWave++;
            waveTimestamp = Time.realtimeSinceStartup;
            switch (currentWave)
            {
                case 1:
                    SpawnEnemyGroups(enemyPrefab, 1, 3750, 8);
                    break;
                case 2:
                    SpawnEnemyGroups(enemyPrefab, 1, 2000, 15);
                    break;
                case 3:
                    SpawnEnemyGroups(enemyPrefab, 1, 4000, 7);
                    SpawnEnemyGroups(enemyPrefab, 1, 3000, 10);
                    
                    break;
                case 4:
                    SpawnEnemyGroups(enemyPrefab, 1, 2500, 12);
                    SpawnEnemyGroups(enemyPrefab, 1, 2000, 15);
                    break;
                case 5:
                    SpawnEnemyGroups(enemyPrefab, 1, 2000, 15);
                    SpawnEnemyGroups(enemyPrefab, 1, 1750, 17);

                    break;
                default:
                    //Debug.LogError("Wave " + currentWave + " has not been made/configured!");
                    break;
            }
        }
    }

    private void SpawnEnemyGroups(
        GameObject enemyType, 
        int groupSize, 
        int msBetweenGroups, 
        int numberOfGroups)
    {
        // gameObject is auto created by unity, and points towards the object this
        // script is attached to (Game Manager object)
        gameObject.AddComponent<GroupSpawner>().Initialize(
        enemyType, 
        groupSize, 
        msBetweenGroups, 
        numberOfGroups);
;    }
}
