using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// GroupSpawner spawns groups of one enemy type numberOfGroups amount of 
/// times, then destroys itself.
/// Implemented this way to allow for more unique waves, e.g. wave 1 spawns a 
/// group of 4 Enemy1 evry 5 seconds, while also spawning an Enemy2 every
/// 7 seconds
/// A "group" is a group of enemies spawned in unison, no matter the size 
/// (Even if that size is 1)
/// </summary>
public class GroupSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyType;
    [SerializeField]
    private int groupSize;
    [SerializeField]
    private int msBetweenGroups;
    [SerializeField]
    private int numberOfGroups;

    float groupSpawnTimestamp;
    WaveManager waveManager;

    private float maxHealthIncreaseOverride;
    private int moneyValuesOverride;
    private float moveSpeedIncreaseOverride;

    /// <summary>
    /// "Constructor" for GroupSpawner
    /// </summary>
    public void Initialize(
        GameObject enemyType,
        int groupSize,
        int msBetweenGroups,
        int numberOfGroups, float maxHealthIncreaseOverride = -1, int moneyValuesOverride = -1, float moveSpeedIncreaseOverride = -1)
    {
        this.enemyType = enemyType;
        this.groupSize = groupSize;
        this.msBetweenGroups = msBetweenGroups;
        this.numberOfGroups = numberOfGroups;
        this.maxHealthIncreaseOverride = maxHealthIncreaseOverride;
        this.moneyValuesOverride = moneyValuesOverride;
        this.moveSpeedIncreaseOverride = moveSpeedIncreaseOverride;

        waveManager = GameManager.Instance.WaveManager;
        // First waves spawns instantly
        groupSpawnTimestamp = Time.time - msBetweenGroups / 1000;
        //Debug.Log(groupSpawnTimestamp);
    }

    /// <summary>
    /// Returns a random spawn point (at index 0) and its associated 
    /// spawnPointOffset (at index 1) from waveManager.EnemySpawnPositions
    /// </summary>
    private Vector3[] GetRandomSpawnPosition()
    {
        int i = Random.Range(0, waveManager.EnemySpawnPositions.Count() - 1);
        try
        {
            Transform tempTransform = waveManager.EnemySpawnPositions[Random.Range(0, i)];
            Vector3[] returnVector = new Vector3[2];
            returnVector[0] = new Vector3(tempTransform.position.x, 0, tempTransform.position.y);
            returnVector[1] = tempTransform.GetComponentInParent<SpawnVector>().SpawnVectorOffset;
            return returnVector;
        }
        catch
        {
            Debug.LogError("GroupSpawner.GetRandomSpawnPosition() encountered an error! Aborting.");
            // Deletes this script
            Destroy(this);
            return null;
        }
    }

    /// <summary>
    /// Spawns a x amount of enemies at a single random spawn point
    /// </summary>
    private void SpawnGroup()
    {
        for (int i = 0; i < groupSize; i++)
        {
            GameObject enemyObj = null;
            try
            {
                enemyObj = Object.Instantiate(enemyType);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                GameManager.Instance.enemies.Add(enemy);
                if (maxHealthIncreaseOverride != -1) enemy.IncreaseMaxHealthBy(maxHealthIncreaseOverride);
                if (moneyValuesOverride != -1) enemy.UpdateMoneyValue(moneyValuesOverride);
                if (moveSpeedIncreaseOverride != -1) enemy.IncreaseSpeedBy(moveSpeedIncreaseOverride);
            }
            catch
            {
                Debug.LogError("GroupSpawner.SpawnGroup() could not instantiate an enemy! Aborting.");
                // Deletes this script
                Destroy(this);
            }
            Vector3[] spawnVectors = GetRandomSpawnPosition();
            enemyObj.transform.position = (spawnVectors[0] + spawnVectors[1] * i);
        }
    }

    void Update()
    {
        if (numberOfGroups < 1) { Destroy(this); }
        if (Time.time > groupSpawnTimestamp + msBetweenGroups / 1000)
        {
            groupSpawnTimestamp = Time.time;
            SpawnGroup();
            numberOfGroups--;
        }
    }
}
