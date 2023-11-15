using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    protected int damage = 10;

    [SerializeField]
    protected float moveSpeed = 1.0f;

    [SerializeField]
    protected int moneyValue = 1;

    protected int waypointIndex = 0;

    protected Enemy enemyComponent;
    protected SpriteRenderer sprite;

    protected Transform[] path;

    private bool isFrozen;

    private void Awake() {
        enemyComponent = GetComponent<Enemy>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isFrozen = false;
    }

    protected override void Start() {
        base.Start();
        path = GameManager.Instance.GetRandomPath();
        //transform.position = path[waypointIndex].transform.position;
    }

    private void Update() {
        if (!isFrozen) Pathfind();
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

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("DivinePillar")) {
            collision.gameObject.GetComponent<DivinePillar>().TakeDamage(damage);
            GameManager.Instance.RemoveEnemy(enemyComponent);
            Destroy(gameObject);
        }
    }

    public void Freeze (float freezeDuration) {
        StartCoroutine(FreezeCoroutine(freezeDuration));
    }

    IEnumerator FreezeCoroutine(float freezeDuration) {
        isFrozen = true;

        yield return new WaitForSeconds(freezeDuration);

        isFrozen = false;
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + new Vector3(-.2f, .35f, 0), Health.ToString());
    }
}
