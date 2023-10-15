using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyPrefabs;
    [SerializeField]
    float elapsedTime;
    [SerializeField]
    float previousFrameElapsedTime;

    Camera camera;

    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        enemies = new List<GameObject>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        previousFrameElapsedTime = elapsedTime;
        elapsedTime += Time.deltaTime;
        // If the previous frame was before the 10s mark and the current frame is
        // after it, spawn enemies once. (Does not break on low FPS)
        if (previousFrameElapsedTime % 10 > elapsedTime % 10)
        {
            // TODO: if (night)
            SpawnWave((int)elapsedTime);
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
            Debug.Log("Wave budget: " + budget + "Spending " + enemyPrefabs[id].GetComponent<EnemyInfo>().SpawnPoints + " on new enemy.");

            budget -= enemyPrefabs[id].GetComponent<EnemyInfo>().SpawnPoints;
            enemies.Add(
                Instantiate(
                    enemyPrefabs[id], 
                    new Vector3(
                        waveCenterPoint.x + (float)Random.Range(0, 300) / 100, 
                        waveCenterPoint.y + (float)Random.Range(0, 300) / 100, 
                        0.0f), 
                    new Quaternion()));
        }
    }
}
