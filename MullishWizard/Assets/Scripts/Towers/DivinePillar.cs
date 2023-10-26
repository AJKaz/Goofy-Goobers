using UnityEngine;
using UnityEngine.SceneManagement;

public class DivinePillar : Tower
{

    protected override void Update() {
        // Overridden to not shoot
    }
    protected override void Die() {
        Destroy(gameObject);
        SceneManager.LoadSceneAsync("GameOverScene");
    }

    // Brought these methods over, can eventually have cool custom logic
    // for the divine pillar shooting enemies if it's taking damage, could make
    // for an interesting feature in the game
    /*protected override void Shoot() {
       
    }

    protected override GameObject GetTarget() {
        
    }*/
}
