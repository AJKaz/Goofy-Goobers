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
        if (!GameManager.Instance.isInOnboarding && GameManager.Instance.enemies.Count < 1 &&
            Time.realtimeSinceStartup > MinTimeBetweenWaves + waveTimestamp)
        {
            currentWave++;
            waveTimestamp = Time.realtimeSinceStartup;
            spellActivateScript.ResetAllSpellCooldowns();
            // At the moment, I've tried to keep waves at ~30s
            switch (currentWave)
            {
                case 1: // 8, 0, 0
                    // 8 basic enemies in groups of 1, 3.75s between each group
                    // total wave spawn time: 30s
                    SpawnEnemyGroups(enemyType: enemyPrefabs[0], groupSize: 1, msBetweenGroups: 3750, numberOfGroups: 8);
                    break;                      
                case 2: // 12, 0, 0
                    GameManager.Instance.AddMoney(40);
                    // 12 basic enemies in groups of 2, 5s between each group
                    // total wave spawn time: 30s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 2, 5000, numberOfGroups: 6);
                    break;                      
                case 3: // 18, 12, 0
                    GameManager.Instance.AddMoney(75);
                    // 18 basic enemies in groups of 6, 10s between groups
                    // 12 fast enemies in groups of 2, 5s between groups
                    // total wave spawn time: 30s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 6, 10000, numberOfGroups: 3);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: 2, 5000, numberOfGroups: 6);
                    break;                      
                case 4: // 24, 30, 6
                    GameManager.Instance.AddMoney(100);
                    // 24 basic enemies in groups of 2, 2.5s between groups
                    // 30 fast enemies in groups of 3, 3s between groups
                    // 6 tank enemies in groups of 1, 6s between groups
                    // total wave spawn time: 30s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 2, 2500, numberOfGroups: 12);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: 3, 3000, numberOfGroups: 10);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 1, 6000, numberOfGroups: 5);
                    break;
                case 5: // 41, 36, 15
                    GameManager.Instance.AddMoney(125);
                    // 36 basic enemies in groups of 3, 2.5s between groups
                    // 5 basic enemies in groups of 1, 6s between groups
                    // 36 fast enemies in groups of 3, 2.5s between groups
                    // 10 tank enemies in groups of 1, 3s between groups
                    // 5 tank enemies in groups of 1, 6s between groups
                    // total wave spawn time: 30s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 3, 2500, numberOfGroups: 12);
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 1, 6000, numberOfGroups: 5);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: 3, 2500, numberOfGroups: 12);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 1, 3000, numberOfGroups: 10);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 1, 6000, numberOfGroups: 5);
                    break;
                case 6: // 60, 54, 30
                    GameManager.Instance.AddMoney(150);
                    // 60 Basic enemies in groups of 4, 3s between groups
                    // 54 fast enemies in groups of 3, 2.5s between groups
                    // 30 tank enemies in groups of 2, 3s between group
                    // total wave time: 45s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 4, 3000, numberOfGroups: 15);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: 3, 2500, numberOfGroups: 18);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 2, 3000, numberOfGroups: 15);
                    break;
                case 7: // 75, 60, 41
                    GameManager.Instance.AddMoney(175);
                    // 75 Basic enemies in groups of 4, 3s between groups
                    // 60 fast enemies in groups of 6, 4.5s between groups
                    // 36 tank enemies in groups of 4, 5s between groups
                    // 5 tank enemies in groups of 1, 9s between groups
                    // total wave time: 45s
                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: 5, 3000, numberOfGroups: 15);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: 6, 4500, numberOfGroups: 10);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 4, 5000, numberOfGroups: 9);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: 1, 9000, numberOfGroups: 5);
                    break;
                default:
                    GameManager.Instance.divinePillar.IncreaseHealthBy(1);
                    if (currentWave == 8) SetAllEnemyMoneysToOne();
                    // For every wave beyond 7:
                    int moneyToAdd = 175 + (currentWave - 7) * 25;
                    if (moneyToAdd > 250) moneyToAdd = 250;
                    GameManager.Instance.AddMoney(moneyToAdd);

                    // Each wave increase enemy health by 1-5 each wave
                    if (currentWave < 10) IncreaseAllEnemyMaxHealthBy(1);
                    else if (currentWave < 15) IncreaseAllEnemyMaxHealthBy(2);
                    else if (currentWave < 20) IncreaseAllEnemyMaxHealthBy(3);
                    else if (currentWave < 25) IncreaseAllEnemyMaxHealthBy(4);
                    else IncreaseAllEnemyMaxHealthBy(5);

                    IncreaseAllEnemySpeed();

                    int basicGroupSize = 4 + (currentWave - 4); // Increase group size by 4 every wave
                    int fastGroupSize = 2 + (currentWave - 4); // Increase group size by 2 every wave
                    int tankGroupSize = 1 + (currentWave - 4); // Increase group size by 1 every wave

                    int basicEnemies = 8 + (currentWave - 3) * 9; // Increase by 9 each wave
                    int fastEnemies = 6 + (currentWave - 3) * 8; // Increase by 8 each wave
                    int tankEnemies = 4 + (currentWave - 3) * 6; // Increase by 6 each wave

                    int basicDelay = 3000 - (currentWave - 7) * 200; // Decrease delay by 200ms each wave
                    int fastDelay = 2500 - (currentWave - 7) * 200; // Decrease delay by 200ms each wave
                    int tankDelay = 3000 - (currentWave - 7) * 200; // Decrease delay by 200ms each wave

                    SpawnEnemyGroups(enemyPrefabs[0], groupSize: basicGroupSize, basicDelay, basicEnemies / basicGroupSize);
                    SpawnEnemyGroups(enemyPrefabs[1], groupSize: fastGroupSize, fastDelay, fastEnemies / fastGroupSize);
                    SpawnEnemyGroups(enemyPrefabs[2], groupSize: tankGroupSize, tankDelay, tankEnemies / tankGroupSize);
                    break;
            }
        }
    }

    private void IncreaseAllEnemyMaxHealthBy(int amountToIncrease) {
        foreach (var enemy in enemyPrefabs) {
            enemy.GetComponent<Enemy>().IncreaseMaxHealthBy(amountToIncrease);
        }
    }

    private void IncreaseAllEnemySpeed() {
        foreach (var enemy in enemyPrefabs) {
            enemy.GetComponent<Enemy>().IncreaseSpeedBy(0.05f);
        }
    }

    private void SetAllEnemyMoneysToOne() {
        foreach (var enemy in enemyPrefabs) {
            enemy.GetComponent<Enemy>().UpdateMoneyValue(1);
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
