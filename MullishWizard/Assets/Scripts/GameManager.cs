using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [HideInInspector] public List<GameObject> enemies;
    [HideInInspector] public GameObject[,] towers;
    #region References to other objects and components
    private Grid grid;
    private ThingSpawner thingSpawner;
    private Light lightSource;
    // This isnt used yet, so I've commented it out.
    //public PlayerMovement playerMovement;
    private PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory { get { return playerInventory; } }
    #endregion

    #region Day/Night Cycle variables
    [SerializeField] private float elapsedTime;
    public float ElapsedTime { get { return elapsedTime; } }
    [SerializeField] private float dayNightCycleChangeTimestamp;
    public float DayNightCycleChangeTimestamp { get { return dayNightCycleChangeTimestamp; } }
    [SerializeField] private bool isNight;
    public bool IsNight { get {  return isNight; } }
    #endregion

    private void Awake() {
        grid = GameObject.Find("TowerManager").GetComponent<Grid>();
        thingSpawner = GameObject.Find("ThingSpawner").GetComponent<ThingSpawner>();
        lightSource = GameObject.Find("DirectionalLight").GetComponent<Light>();
        // This isnt used yet, so I've commented it out.
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() 
    {
        enemies = new List<GameObject>();
        towers = new GameObject[grid.width, grid.height];
        elapsedTime = 0;
        dayNightCycleChangeTimestamp = 0;
        lightSource.intensity = 0.4f;
        thingSpawner.BeginDay();
        isNight = false;
        Debug.Log("isNight variable initialized to: " + isNight + ".");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        DayNightCycleUpdate();
    }

    public void CreateTower(GameObject towerPrefab, int gridX, int gridY) {
        // If grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0) {
            // If the tower prefab exists and there is not currently a tower there, create one
            if (towerPrefab != null && towers[gridX, gridY] == null) {
                GameObject tower = Instantiate(towerPrefab, grid.GetWorldPosition(gridX, gridY) + new Vector3(grid.cellSize / 2f, grid.cellSize / 2f), Quaternion.identity);
                tower.transform.SetParent(transform, true);
                towers[gridX, gridY] = tower;
            }
        }
    }

    public void DestroyTower(int gridX, int gridY) {
        // If the grid coordinates are within bounds
        if (gridX < grid.width && gridX >= 0 && gridY < grid.height && gridY >= 0) {
            // If a tower is there, destroy it
            if (towers[gridX, gridY] != null) {
                Destroy(towers[gridX, gridY]);
            }
        }
    }

    /*public bool RemoveTower(int x, int y) {
        return towers[x, y];
    }*/

    public bool RemoveEnemy(GameObject enemy) {
        return enemies.Remove(enemy);
    }

    private void DayNightCycleUpdate()
    {
        if (!isNight && elapsedTime > dayNightCycleChangeTimestamp + 15)
        {
            Debug.Log("Night is beginning");
            thingSpawner.BeginNight();
            lightSource.intensity = 0.0f;
            dayNightCycleChangeTimestamp = elapsedTime;
            isNight = true;
            Debug.Log("Night has begun");
        } 
        else if (isNight && thingSpawner.AllWavesSpawned && enemies.Count == 0)
        {
            Debug.Log("Day is beginning");
            thingSpawner.BeginDay();
            lightSource.intensity = 0.4f;
            dayNightCycleChangeTimestamp = elapsedTime;
            isNight = false;
            Debug.Log("Day has begun");
        }
    }
}
