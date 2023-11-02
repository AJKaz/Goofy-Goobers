using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager InputManager { get; private set; }
    public Grid Grid { get; private set; }

    [HideInInspector] 
    public List<Enemy> enemies;

    [SerializeField]
    private Transform[] path1;

    [SerializeField]
    private Transform[] path2;

    [SerializeField]
    private Transform[] path3;

    [SerializeField]
    private int startingMoney = 10;

    private int money;

    public int Money { get { return money; } }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        money = startingMoney;

        InputManager = GetComponent<InputManager>();
        Grid = GetComponent<Grid>();

        // Find all grid tiles in which there is a path and set it to 1
        foreach (Transform transform in path1)
        {
            // A 1 in the grid represents a path
            Grid.SetValue(transform.position, 1);
        }
        foreach (Transform transform in path2)
        {
            Grid.SetValue(transform.position, 1);
        }
        foreach (Transform transform in path3)
        {
            Grid.SetValue(transform.position, 1);
        }
    }

    public Transform[] GetRandomPath() {
        int randomPath = Random.Range(0, 2);
        switch (randomPath) {
            case 0: return path1;
            case 1: return path2;
            case 2: return path3;
            default: return path1;
        }
    }

    public void AddMoney(int amount) {
        money += amount;
    }

    public bool RemoveEnemy(Enemy enemy) {
        return enemies.Remove(enemy);
    }

}
