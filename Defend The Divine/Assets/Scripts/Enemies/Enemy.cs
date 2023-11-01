using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    protected float damage = 10.0f;

    [SerializeField]
    protected float moveSpeed = 1.0f;

    [SerializeField]
    protected int moneyValue = 1;

    protected int waypointIndex = 0;

    protected Enemy enemyComponent;
    protected SpriteRenderer sprite;

    protected Transform[] path;

    private void Awake() {
        enemyComponent = GetComponent<Enemy>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();
        path = GameManager.Instance.GetRandomPath();
        //transform.position = path[waypointIndex].transform.position;
    }

    void Update() {
        Pathfind();
    }

    protected void Pathfind() {
        if (waypointIndex < path.Length) {
            // Move enemy from current waypoint to next one
            transform.position = Vector2.MoveTowards(transform.position,
                path[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            // When enemy reaches next waypoint, increase waypointIndex so they can walk to next one
            if (transform.position == path[waypointIndex].transform.position) {
                waypointIndex++;
            }
        }
    }

    override protected void Die() {
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(enemyComponent);
        GameManager.Instance.AddMoney(moneyValue);
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("DivinePillar")) {
            Debug.Log("enemy DivinePillar enter");
        }
    }
}
