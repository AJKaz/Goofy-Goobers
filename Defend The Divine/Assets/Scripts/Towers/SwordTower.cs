using UnityEngine;

public class SwordTower : Tower
{
    [SerializeField]
    private float rotationSpeed = 360f;

    [SerializeField]
    private SpriteRenderer swordSpriteRenderer;

    private bool isSwinging = false;
    private float currentRotation = 0f;
    private Quaternion initialRotation;

    private void Start() {
        initialRotation = damagingPrefab.transform.rotation;
        if (swordSpriteRenderer == null) {
            swordSpriteRenderer = damagingPrefab.GetComponent<SpriteRenderer>();
        }
        swordSpriteRenderer.enabled = false;
    }

    protected override void Update() {
        base.Update();
        if (isSwinging) {
            ContinueSwing();
        }
    }

    private void ContinueSwing() {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;
        damagingPrefab.transform.RotateAround(transform.position, Vector3.back, rotationThisFrame);
        currentRotation += rotationThisFrame;
        if (currentRotation >= 360f) {
            isSwinging = false;
            currentRotation = 0f;
            damagingPrefab.transform.rotation = initialRotation;
            swordSpriteRenderer.enabled = false;
        }
    }

    protected override void Attack(Enemy target) {
        shootTimer = SHOOT_DELAY;
        ContinueSwing();
        isSwinging = true;
        swordSpriteRenderer.enabled = true;

        // Damage all enemies in range
        // Using this instead of colliders because:
        // Even with Continuous colliders, it still missed enemies when they were clumped together
        // Was just pretty buggy, this works 100% of the time
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D enemyCollider in enemies) {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                enemy.BloodSplatRotation = null;
            }
        }
    }

    protected override Enemy GetTarget() {
        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) {
            if (GameManager.Instance.enemies[i] == null) continue;
            Vector3 offset = transform.position - GameManager.Instance.enemies[i].transform.position;
            if (offset.sqrMagnitude <= range * range) {
                return GameManager.Instance.enemies[i];
            }
        }
        return null;
    }
}
