using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

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

    private Tower parentTower;

    public void SetTarget(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void SetStats(float damage, float speed, Tower parentTower)
    {
        this.damage = damage;
        this.speed = speed;
        this.parentTower = parentTower;
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
        if (!hasHitEnemy && collision.gameObject.CompareTag("Enemy"))
        {
            hasHitEnemy = true;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                if (enemy.TakeDamage(damage)) {
                    parentTower.KilledEnemy();
                }
                enemy.BloodSplatRotation = transform.rotation.eulerAngles.z + 180 % 360;
            }

            //ParticleSystem bloodSplatter = collision.gameObject.GetComponentsInChildren<ParticleSystem>().FirstOrDefault(p => p.name == "Directional Blood Splat");
            //if (bloodSplatter)
            //{
            //    bloodSplatter.transform.rotation = Quaternion.Euler(0, 0, 0);
            //    bloodSplatter.Play();
            //}
            Destroy(gameObject);
        }
    }
}
