using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Enemy : Entity
{
    public GameObject textPrefab;
    [SerializeField]
    protected int damage = 10;

    [SerializeField]
    protected float moveSpeed = 1.0f;

    [SerializeField]
    protected int moneyValue = 1;

    [SerializeField]
    [ColorUsage(true)]
    protected GameObject bloodSplatter;

    [SerializeField]
    protected Color damageFlash = Color.red;
    protected Color baseColor;

    [SerializeField] private DemonEssence demonEssence;

    protected int waypointIndex = 0;

    protected SpriteRenderer sprite;

    protected Transform[] path;

    private bool isFrozen;

    private Vector2 direction;
    private Animator animator;

    [HideInInspector] public float? BloodSplatRotation { get; set; } = null;

    public int WaypointIndex { get { return waypointIndex; } }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        isFrozen = false;
        animator = GetComponentInChildren<Animator>();
        baseColor = sprite.color;
    }

    protected override void Start()
    {
        base.Start();
        path = GameManager.Instance.GetPath();
    }

    private void Update()
    {
        if (!isFrozen) Pathfind();
        Animate();
    }

    protected void Pathfind()
    {
        if (waypointIndex < path.Length)
        {
            // Move enemy from current waypoint to next one
            transform.position = Vector2.MoveTowards(transform.position,
                path[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            direction = path[waypointIndex].transform.position - transform.position;

            // When enemy reaches next waypoint, increase waypointIndex so they can walk to next one
            if (transform.position == path[waypointIndex].transform.position)
            {
                waypointIndex++;
            }
        }
    }

    public override bool TakeDamage(float damage)
    {
        GameObject text = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = damage.ToString();

        StartCoroutine(DamageFlashCoroutine(0.1f));
        return base.TakeDamage(damage);
    }

    override protected void Die()
    {
        if (bloodSplatter)
        {
            string splatterEffect = BloodSplatRotation == null ? "Blood Splat" : "Directional Blood Splat";
            ParticleSystem splat = bloodSplatter.GetComponentsInChildren<ParticleSystem>().FirstOrDefault(ps => ps.name == splatterEffect);
            Instantiate(splat, transform.position, Quaternion.Euler(0, 0, BloodSplatRotation ?? 0)).Play();
        }
        if (demonEssence) {
            Instantiate(demonEssence, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.AddMoney(moneyValue);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DivinePillar"))
        {
            collision.gameObject.GetComponent<DivinePillar>().TakeDamage(damage);
            GameManager.Instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void Freeze(float freezeDuration)
    {
        StartCoroutine(FreezeCoroutine(freezeDuration));
    }

    private void Animate()
    {
        int animationAngle = 90;
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        if (angle > 360) angle -= 360;
        animationAngle = angle >= 345 && angle < 375 || angle >= -15 && angle < 15 ? 0 :
                         angle >= 15 && angle < 37.5f ? 15 :
                         angle >= 37.5f && angle < 52.5f ? 45 :
                         angle >= 52.5f && angle < 75 ? 60 :
                         angle >= 75 && angle < 105 ? 90 :
                         angle >= 105 && angle < 127.5f ? 120 :
                         angle >= 127.5f && angle < 142.5f ? 135 :
                         angle >= 142.5f && angle < 165 ? 150 :
                         angle >= 165 && angle < 195 ? 180 :
                         angle >= 195 && angle < 217.5f ? 210 :
                         angle >= 217.5f && angle < 232.5f ? 225 :
                         angle >= 232.5f && angle < 255 ? 240 :
                         angle >= 255 && angle < 285 ? 270 :
                         angle >= 285 && angle < 307.5f ? 300 :
                         angle >= 307.5f && angle < 322.5f ? 315 :
                         angle >= 322.5f && angle < 345 ? 330 :
                         animationAngle;
        animator?.SetInteger("angle", animationAngle);

    }

    IEnumerator FreezeCoroutine(float freezeDuration)
    {
        isFrozen = true;
        if (animator) animator.speed = 0;

        yield return new WaitForSeconds(freezeDuration);

        isFrozen = false;
        if (animator) animator.speed = 1;
    }

    IEnumerator DamageFlashCoroutine(float seconds = 0.1f)
    {
        sprite.color = damageFlash;
        yield return new WaitForSeconds(seconds);
        sprite.color = baseColor;
    }

    public void UpdateMoneyValue(int newMoneyValue) {
        moneyValue = newMoneyValue;
    }

    public void IncreaseMaxHealthBy(int amountToIncrease) {
        maxHealth += amountToIncrease;
    }

    public void IncreaseSpeedBy(float amountToIncrease) {
        moveSpeed += amountToIncrease;
    }

    //private void OnDrawGizmos()
    //{
    //    Handles.Label(transform.position + new Vector3(-.2f, .35f, 0), Health.ToString());
    //}
}
