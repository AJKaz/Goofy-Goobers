using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs = new GameObject[3];

    [SerializeField]
    private Transform[] enemySpawnPositions;
    public Transform[] EnemySpawnPositions { get { return enemySpawnPositions; } }
    
    [SerializeField]
    private int currentWave;
    private const int MinTimeBetweenWaves = 10;
    private float waveTimestamp = -(MinTimeBetweenWaves/2);
    private SpellActivate spellActivateScript;

    private void Awake() {
        if (enemySpawnPositions.Length == 0) {
            Debug.LogError("Enemy spawn positions array is empty in WaveManager");
        }
        currentWave = 0;
        spellActivateScript = gameObject.GetComponent<SpellActivate>();
    }

    void Update()
    {
        // This code runs each time a wave ends
        if (GameManager.Instance.enemies.Count < 1 &&
            Time.realtimeSinceStartup > MinTimeBetweenWaves + waveTimestamp)
        {
            currentWave++;
            waveTimestamp = Time.realtimeSinceStartup;
            spellActivateScript.ResetAllSpellCooldowns();
            // At the moment, I've tried to keep waves at ~30s
            switch (currentWave)
            {
                case 1:
                    SpawnEnemyGroups(enemyPrefabs[0], 1, 3750, 8);
                    break;                      
                case 2:                         
                    SpawnEnemyGroups(enemyPrefabs[0], 2, 5000, 6);
                    break;                      
                case 3:
                    SpawnEnemyGroups(enemyPrefabs[0], 10, 10000, 3);
                    SpawnEnemyGroups(enemyPrefabs[1], 2, 5000, 6);
                    break;                      
                case 4:                         
                    SpawnEnemyGroups(enemyPrefabs[1], 3, 2500, 12);
                    SpawnEnemyGroups(enemyPrefabs[1], 2, 2000, 15);
                    break;                      
                case 5:                         
                    SpawnEnemyGroups(enemyPrefabs[0], 5, 3000, 15);
                    SpawnEnemyGroups(enemyPrefabs[2], 3, 3000, 15);
                    break;
                case 6:
                    SpawnEnemyGroups(enemyPrefabs[2], 6, 5000, 6);
                    break;
                default:
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
        // gameObject is auto created by unity, and points towards the object
        // this script is attached to (Game Manager object)
        gameObject.AddComponent<GroupSpawner>().Initialize(
        enemyType, 
        groupSize, 
        msBetweenGroups, 
        numberOfGroups);
;    }
}
