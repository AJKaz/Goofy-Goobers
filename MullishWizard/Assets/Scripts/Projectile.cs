using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPosition;

    [SerializeField]
    private float damage = 5;

    [SerializeField]
    private float speed = 15f;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float despawnTimer = 0.9f;

    private void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // TODO: Projectile should shoot in a straight line and keep going for a limited range defined via variables
        transform.position += direction * speed * Time.deltaTime;

        //float angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg - 90;
        //transform.eulerAngles = transform.forward * angle;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyInfo>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
