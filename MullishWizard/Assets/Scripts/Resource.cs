using UnityEngine;

public class Resource : MonoBehaviour {
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private int quantity;
    [SerializeField] private PlayerInventory playerInventory;

    // This is the "cost" of ThingManager spawning this resource.
    private short spawnPoints;
    public short SpawnPoints { get { return spawnPoints; } }

    private void Awake()
    {
        spawnPoints = 10;
    }

    private void Start()
    {
        // This must be in Start() as GameManager intializes PlayerInventory in
        // Awake(), so if this runs any earlier it will be unassigned
        playerInventory = GameManager.Instance.PlayerInventory;
        // Failsafes
/*        if (resourceType != ResourceType.Scrap &&
            resourceType != ResourceType.Wood) { 
            resourceType = ResourceType.Scrap; 
        }*/
        if (quantity == 0) { quantity = 1; }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        playerInventory.AddResources(resourceType, quantity);
        Destroy(gameObject);
    }
}
