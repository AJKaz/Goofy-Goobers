using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] 
    public List<Enemy> enemies;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public bool RemoveEnemy(Enemy enemy) {
        return enemies.Remove(enemy);
    }

}
