using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> resourcePrefabs;
    [SerializeField] private short nightsSurvived;
    [SerializeField] private float waveSpawnTimestamp;
    [SerializeField] private short wavesToSpawn;
    // Returns true if 0 waves in queue
    public bool AllWavesSpawned { get { return wavesToSpawn < 1; } }

    void Awake()
    {
        // BeginDay() is called by GameManager when scene loads, incrementing
        // this value by +1
        nightsSurvived = -1;
        waveSpawnTimestamp = 0;
        wavesToSpawn = 0;
    }

    void Update()
    {
        // Waves spawn once every 5s on night 1, but 0.1s faster for every night survived
        if (GameManager.Instance.IsNight &&
            GameManager.Instance.ElapsedTime >= waveSpawnTimestamp + 5 - 0.1 * nightsSurvived &&
            wavesToSpawn > 0)
        {
            SpawnWave(nightsSurvived * 10 + 10);
        }
    }

    /// <summary>
    /// Spawns a wave of enemies at a random point. 
    /// Reduces wavesToSpawn by 1.
    /// </summary>
    /// <param name="budget">The amount of spawn points allocated to this wave. 
    /// More points = stronger wave.</param>
    void SpawnWave(int budget)
    {
        // Selects a random direction for the wave to spawn
        float waveDirectionRad = Random.Range(0, 360) * (Mathf.PI / 180);
        Vector2 waveCenterPoint = new Vector2(
            Mathf.Cos(waveDirectionRad) * 15,
            Mathf.Sin(waveDirectionRad) * 15);

        // If the wave spawn center is within 3 units of any camera boundary, move it away
        if (Mathf.Abs(waveCenterPoint.x - Camera.main.transform.position.x) < 12 &&
            Mathf.Abs(waveCenterPoint.y - Camera.main.transform.position.y) < 8)
        {
            waveCenterPoint = 1.25f * waveCenterPoint;
        }

        // Spawn loop
        while (budget > 0)
        {
            short id = (short)Random.Range(0, enemyPrefabs.Count);
            try
            {
                budget -= enemyPrefabs[id].GetComponent<Enemy>().SpawnPoints;
                GameObject enemyObject = Instantiate(
                        enemyPrefabs[id],
                        new Vector3(
                            waveCenterPoint.x + (float)Random.Range(0, 300) / 100,
                            waveCenterPoint.y + (float)Random.Range(0, 300) / 100,
                            0.0f),
                        new Quaternion());
                GameManager.Instance.enemies.Add(enemyObject.GetComponent<Enemy>());
            }
            catch
            {
                budget = 0;
                Debug.Log("enemyPrefabs list is EMPTY! Enemies will not spawn.");
            }

        }
        waveSpawnTimestamp = GameManager.Instance.ElapsedTime;
        wavesToSpawn--;
    }

    /// <summary>
    /// Spawns resources at random locations around the map.
    /// </summary>
    /// <param name="budget">The amount of allocated spawn points. More points = more resources.</param>
    void SpawnResources(int budget)
    {
        // Spawn loop
        while (budget > 0)
        {
            short id = (short)Random.Range(0, resourcePrefabs.Count);
            //try { 
            Debug.Log(resourcePrefabs[id].GetComponent<Resource>().SpawnPoints);
            budget -= resourcePrefabs[id].GetComponent<Resource>().SpawnPoints;
            // Selects a random direction for the wave to spawn
            float spawnDirectionRad = Random.Range(0, 360) * (Mathf.PI / 180);
            Vector2 spawnPoint = new Vector2(
                Mathf.Cos(spawnDirectionRad) * (12 + Random.Range(0f, 8f)),
                Mathf.Sin(spawnDirectionRad) * (12 + Random.Range(0f, 8f)));

            // If the wave spawn center is within 3 units of any camera boundary, move it away
            while (Mathf.Abs(spawnPoint.x - Camera.main.transform.position.x) < 12 &&
                Mathf.Abs(spawnPoint.y - Camera.main.transform.position.y) < 8)
            {
                spawnPoint = spawnPoint * 1.1f;
            }

            Instantiate(
                    resourcePrefabs[id],
                    new Vector3(spawnPoint.x, spawnPoint.y, 0.0f),
                    new Quaternion());
            //}
            //catch {
            //    budget = 0;
            //    Debug.Log("resourcePrefabs list is EMPTY! Resources will not spawn."); 
            //}
            Debug.Log("Spawned resources");
        }
    }

    // Performs one-time actions at day start
    public void BeginDay()
    {
        nightsSurvived++;
        SpawnResources(nightsSurvived * 20);
    }
    // Performs one-time actions at night start
    public void BeginNight()
    {
        wavesToSpawn = (short)(nightsSurvived + 2);
        SpawnWave(nightsSurvived * 10 + 10);
    }
}
