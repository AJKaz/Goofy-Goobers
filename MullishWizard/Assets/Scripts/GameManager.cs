using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [HideInInspector]
    public List<GameObject> enemies;
    // public List<Enemy> Enemies;

    [HideInInspector]
    public List<Tower> towers;

    public PlayerMovement player;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public bool RemoveTower(Tower tower) {
        return towers.Remove(tower);
    }

    public bool RemoveEnemy(GameObject enemy) {
        return enemies.Remove(enemy);
    }

}
