using UnityEngine;

public class IceSpike : MonoBehaviour
{
    [SerializeField]
    private float despawnTimer = 1.0f;

    private Vector3 direction;
    private float damage = 5;
    private float speed = 15f;
    private float damageDecay;

    private Tower parentTower;

    public void SetDirection(Vector3 targetDirection) {
        direction = (targetDirection - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void SetStats(float damage, float speed, Tower parentTower, float damageDecay) {
        this.damage = damage;
        this.speed = speed;
        this.parentTower = parentTower;
        this.damageDecay = damageDecay;
    }

    private void Update() {
        transform.position += direction * speed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy) {
                if (enemy.TakeDamage(damage)) {
                    parentTower.KilledEnemy();
                }
                enemy.BloodSplatRotation = transform.rotation.eulerAngles.z + 180 % 360;
            }
            damage -= damageDecay;
            if (damage <= 0) Destroy(gameObject);
        }
    }
}
