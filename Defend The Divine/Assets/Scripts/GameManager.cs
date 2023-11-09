using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager inputManager;
    public Grid grid;

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

    /* Should be called Demon Essence in UI */
    private int money;

    public GameObject towerUiCanvas;

    [SerializeField]
    private TMP_Text moneyText;

    private WaveManager waveManager;
    public WaveManager WaveManager { get { return waveManager; } }

    public int Money { get { return money; } }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        money = startingMoney;
        waveManager = GetComponent<WaveManager>();

        UpdateMoneyText();
    }
    
    private void Start() {
        // Find all grid tiles in which there is a path and set it to 1
        foreach (Transform transform in path1) {
            // A 1 in the grid represents a path
            grid.SetValue(transform.position, 1);
        }
        foreach (Transform transform in path2) {
            grid.SetValue(transform.position, 1);
        }
        foreach (Transform transform in path3) {
            grid.SetValue(transform.position, 1);
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
        UpdateMoneyText();
    }

    public bool RemoveEnemy(Enemy enemy) {
        return enemies.Remove(enemy);
    }

    public void UpdateMoneyText() {
        moneyText.text = $"${money}";
    }

}
