using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
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

    /// <summary>
    /// "Constructor" for GroupSpawner
    /// </summary>
    public void Initialize(
        GameObject enemyType,
        int groupSize,
        int msBetweenGroups,
        int numberOfGroups)
    {
        this.enemyType = enemyType;
        this.groupSize = groupSize;
        this.msBetweenGroups = msBetweenGroups;
        this.numberOfGroups = numberOfGroups;

        waveManager = GameManager.Instance.WaveManager;
        groupSpawnTimestamp = Time.time;
        Debug.Log(groupSpawnTimestamp);
    }

    /// <summary>
    /// Returns a random spawn point from waveManager.EnemySpawnPositions
    /// </summary>
    private Vector3 GetRandomSpawnPosition()
    {
        int i = Random.Range(0, waveManager.EnemySpawnPositions.Count() - 1);
        try
        {
            Transform tempTransform = waveManager.EnemySpawnPositions[Random.Range(0, i)];
            return new Vector3(tempTransform.position.x, 0, tempTransform.position.y);
        }
        catch
        {
            Debug.LogError("GroupSpawner.GetRandomSpawnPosition() could not return a value! Aborting.");
            Debug.Log("GetRandomSpawnPosition() tried to return index " + i +
                " of EnemySpawnPositions[], which has a size of " +
                waveManager.EnemySpawnPositions.Count());
            // Deletes this script
            Destroy(this);
            return new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// Spawns a x amount of enemies at a single random spawn point
    /// </summary>
    private void SpawnGroup()
    {
        for (int i = 0; i < groupSize; i++)
        {
            GameObject enemy = null;
            try
            {
                enemy = Object.Instantiate(enemyType);
                GameManager.Instance.enemies.Add(enemy.GetComponent<Enemy>());
            }
            catch
            {
                Debug.LogError("Enemy type passed into GroupSpawner is invalid! Aborting.");
                // Deletes this script
                Destroy(this);
            }
            enemy.transform.position = GetRandomSpawnPosition();
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
