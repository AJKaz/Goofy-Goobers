using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    private float damage = 10.0f;

    [SerializeField]
    private float moveSpeed = 2.0f;

    protected Enemy enemyComponent;
    protected SpriteRenderer sprite;

    private void Awake() {
        enemyComponent = GetComponent<Enemy>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        
    }

    override protected void Die() {
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(enemyComponent);
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("DivinePillar")) {
            Debug.Log("enemy DivinePillar enter");
        }
    }
}
