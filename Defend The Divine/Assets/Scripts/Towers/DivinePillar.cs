using TMPro;
using UnityEngine;

public class DivinePillar : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private GameObject fadeToBlack;

    [SerializeField] private TMP_Text healthText;

    private int health;

    public int Health { get { return health; } }

    private void Awake() {
        health = maxHealth;
        TakeDamage(0);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            TowerDestroyed();
        }
        healthText.text = $"HP: {health}";
    }

    /// <summary>
    /// This has it's own function for 2 reasons:
    /// 1 - If we add more game over functionality, it stays neater
    /// 2 - If we have a "2nd chance / 2nd life", we can add that here, stays neater
    /// </summary>
    private void TowerDestroyed() {
        // fadeToBlack will load the game over scene when it is done
        Instantiate(fadeToBlack, new Vector3(0,0,-9.5f), Quaternion.identity);
        //SceneManager.LoadScene("GameOver");
    }
}
