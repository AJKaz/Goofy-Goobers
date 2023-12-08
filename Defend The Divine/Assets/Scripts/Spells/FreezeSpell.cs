using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreezeSpell : MonoBehaviour
{
    [SerializeField] Vector3 initialDirection;

    [SerializeField] private float despawnTimer = 5f;

    [SerializeField] private float freezeDuration = 4f;

    [SerializeField] private float speed = 5f;

    [SerializeField] private int cost = 30;

    private Vector3 direction;

    public int Cost { get { return cost; }  }

    private void Awake() {
        direction.x = (initialDirection - transform.position).normalized.x;
        direction.y = 0f;
        direction.z = 0f;
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
            collision.gameObject.GetComponent<Enemy>().Freeze(freezeDuration);
        }
    }
}
