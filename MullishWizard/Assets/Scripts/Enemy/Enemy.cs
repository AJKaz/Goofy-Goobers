using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float damage = 10.0f;

    /* Use this like a const, can't make it const though cause you can't edit in Unity editor then */
    [SerializeField]
    protected float ATTACK_DELAY = 1.0f;

    protected float attackTimer = 0.05f;

    [SerializeField]
    private float moveSpeed = 2f;

    /*
    * This is the "cost" of ThingManager spawning this unit. ThingManager has a limited
    * budget for each wave, with more expensive units being more powerful. 
    */
    [SerializeField]
    private short spawnPoints = 10;

    [SerializeField]
    private GameObject trackingTarget;

    private Vector3 direction;
    private Vector3 velocity;

    protected Enemy enemyComponent;

    public short SpawnPoints { get { return spawnPoints; } }

    private void Awake() {
        enemyComponent = GetComponent<Enemy>();
    }

    void Update()
    {
        Move(trackingTarget);
    }

    public override void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    override protected void Die() {
        isDead = true;
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(enemyComponent);
    }

    void Move(GameObject target)
    {
        // Get direction of the target
        direction = target.transform.position - transform.position;

        // Get the angle the target is facing and rotate the to face the target
        float angle = Mathf.Atan(direction.y/direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = transform.forward * angle;

        // Move the enemy towards the target
        velocity = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    protected void OnCollisionStay2D(Collision2D collision) {
        attackTimer -= Time.deltaTime;
        if (collision.gameObject.CompareTag("Tower") && attackTimer <= 0.0f) {
            collision.gameObject.GetComponent<Tower>().TakeDamage(damage);
            attackTimer = ATTACK_DELAY;
        }
    }

    // Can be used if we end up having an enemy that "rams" towers for more damage on itial hit
    /* protected void OnCollisionEnter2D(Collision2D collision) {
         if (collision.gameObject.CompareTag("Tower")) {
             //Debug.Log("enemy tower enter");
         }
     }*/
}
