using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DivinePillar : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int health;

    public int Health { get { return health; } }

    private void Awake() {
        health = maxHealth;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            TowerDestroyed();
        }
    }

    /// <summary>
    /// This has it's own function for 2 reasons:
    /// 1 - If we add more game over functionality, it stays neater
    /// 2 - If we have a "2nd chance / 2nd life", we can add that here, stays neater
    /// </summary>
    private void TowerDestroyed() {
        SceneManager.LoadScene("GameOver");
    }
}
