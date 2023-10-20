using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPosition;
    [SerializeField] float damage = 5;
    [SerializeField] GameObject prefab;
    private float despawnTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float moveSpeed = 10f;

        transform.position += direction * moveSpeed * Time.deltaTime;

        float angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg - 90;
        //transform.eulerAngles = transform.forward * angle;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            collision.gameObject.GetComponent<EnemyInfo>().TakeDamage(damage);
            gameObject.SetActive(true);
        }
    }
}
