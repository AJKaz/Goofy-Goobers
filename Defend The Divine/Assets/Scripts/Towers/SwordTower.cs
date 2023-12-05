using UnityEngine;

public class SwordTower : Tower
{
    [Header("Tower Specifics")]
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
        attackTimer = ATTACK_DELAY;
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
                if (enemy.TakeDamage(damage)) {
                    KilledEnemy();
                }
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

    public override void Upgrade() {
        if (upgradeLevel <= maxUpgradeLevel && GameManager.Instance.Money >= upgradeCost) {
            GameManager.Instance.AddMoney(-upgradeCost);
            upgradeLevel++;

            damage += damageUpgradeAmount;
            range += rangeUpgradeAmount;
            ATTACK_DELAY -= attackSpeedUpgradeAmount;

            visibleRange.transform.localScale = new Vector3(range * 2, range * 2);
            swordSpriteRenderer.transform.localScale = new Vector3(swordSpriteRenderer.transform.localScale.x, range * 1.65f);
        }
    }
}
