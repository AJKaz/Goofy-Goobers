using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : MonoBehaviour
{
    [SerializeField]
    private float despawnTimer = 5f;

    private Vector3 direction;
    private float speed = 5f;

    public void SetTarget(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
    }

    public void SetStats(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("FREEZE!");
        }
    }
}
