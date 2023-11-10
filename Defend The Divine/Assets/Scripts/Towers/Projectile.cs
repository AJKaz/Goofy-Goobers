using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float despawnTimer = 0.5f;

    private Vector3 direction;
    private float damage = 5;
    private float speed = 15f;

    private bool hasHitEnemy = false;

    public void SetTarget(Vector3 targetPosition) {
        direction = (targetPosition - transform.position).normalized;
    }

    public void SetStats(float damage, float speed) {
        this.damage = damage;
        this.speed = speed;
    }

    private void Update() {
        transform.position += direction * speed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!hasHitEnemy && collision.gameObject.CompareTag("Enemy")) {
            hasHitEnemy = true;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
