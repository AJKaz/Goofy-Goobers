using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        money = startingMoney;
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
    public int GetMoney() { return money; }

    public bool RemoveEnemy(Enemy enemy) {
        return enemies.Remove(enemy);
    }

}
