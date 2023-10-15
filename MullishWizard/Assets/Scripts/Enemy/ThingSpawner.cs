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
    [SerializeField]
    short nightsSurvived;
    [SerializeField]
    bool isNight;
    short wavesToSpawn;
    Camera camera;
    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        nightsSurvived = 0;
        // Day not implemented yet!
        // isNight = false;
        isNight = true;
        // Debug initialization because day/night cycle is not done yet
        wavesToSpawn = 5;
        camera = Camera.main;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        previousFrameElapsedTime = elapsedTime;
        elapsedTime += Time.deltaTime;        
        if (!isNight)
        {
            // SpawnResource()
            wavesToSpawn = (short)(nightsSurvived + 1);
        }
        if (isNight)
        {
            if (wavesToSpawn > 0 && previousFrameElapsedTime % 5 > elapsedTime % 5)
            {
                SpawnWave((int)elapsedTime);
                wavesToSpawn--;
                Debug.Log(wavesToSpawn + " waves left for this night!");
            }
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
            enemies.Add(
                Instantiate(
                    enemyPrefabs[id],
                    new Vector3(
                        waveCenterPoint.x + (float)Random.Range(0, 300) / 100,
                        waveCenterPoint.y + (float)Random.Range(0, 300) / 100,
                        0.0f),
                    new Quaternion()));
        }
        //Debug.Log("Spawned wave with budget: " + budget);
    }

    /// <summary>
    /// Spawns resources at random locations around the map.
    /// </summary>
    /// <param name="budget">The amount of allocated spawn points. More points = more resources.</param>
    void SpawnResources(int budget)
    {
        // TODO
    }
}
