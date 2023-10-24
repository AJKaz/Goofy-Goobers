using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private short nightsSurvived;
    private Camera camera;
    [SerializeField] private float waveSpawnTimestamp;
    [SerializeField] private short wavesToSpawn;
    // Returns true if 0 waves in queue
    public bool AllWavesSpawned { get { return wavesToSpawn < 1; } }

    void Start()
    {
        nightsSurvived = -1;
        camera = Camera.main;
        waveSpawnTimestamp = 0;
        wavesToSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Waves spawn once every 5s on night 1, but 0.1s faster for every night survived
        if (GameManager.Instance.IsNight && 
            GameManager.Instance.ElapsedTime >= waveSpawnTimestamp + 5 - 0.1*nightsSurvived &&
            wavesToSpawn > 0)
        {
            SpawnWave(nightsSurvived * 10 + 10);
        }
    }

    /// <summary>
    /// Spawns a wave of enemies at a random point.
    /// </summary>
    /// <param name="budget">The amount of spawn points allocated to this wave. More points = stronger wave.</param>
    void SpawnWave(int budget)
    {
        // Selects a random direction for the wave to spawn
        float waveDirectionRad = Random.Range(0, 360) * (Mathf.PI / 180);
        Vector2 waveCenterPoint = new Vector2(
            Mathf.Cos(waveDirectionRad) * 15,
            Mathf.Sin(waveDirectionRad) * 15);

        // If the wave spawn center is within 3 units of any camera boundary, move it away
        if (Mathf.Abs(waveCenterPoint.x - camera.transform.position.x) < 12 &&
            Mathf.Abs(waveCenterPoint.y - camera.transform.position.y) < 8)
        {
            waveCenterPoint = 1.25f * waveCenterPoint;
        }

        // Spawn loop
        while (budget > 0)
        {
            short id = (short)Random.Range(0, enemyPrefabs.Count - 1);
            budget -= enemyPrefabs[id].GetComponent<EnemyInfo>().SpawnPoints;
            GameManager.Instance.enemies.Add(
                Instantiate(
                    enemyPrefabs[id],
                    new Vector3(
                        waveCenterPoint.x + (float)Random.Range(0, 300) / 100,
                        waveCenterPoint.y + (float)Random.Range(0, 300) / 100,
                        0.0f),
                    new Quaternion()));
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
        // TODO
    }

    public void BeginDay()
    {
        nightsSurvived++;
        SpawnResources(nightsSurvived * 20);
    }
    public void BeginNight()
    {
        wavesToSpawn = (short)(nightsSurvived + 2);
        SpawnWave(nightsSurvived * 10 + 10);
    }
}
