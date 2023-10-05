using UnityEngine;

public class Resource : MonoBehaviour {
    [SerializeField] 
    private ResourceType resourceType;

    [SerializeField]
    private int quantity = 1;

    [SerializeField]
    private PlayerInventory playerInventory;


    private void Awake() {
        // TODO: THIS DOESN'T WORK
        if (playerInventory == null) playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        Debug.Log($"collision with resource {resourceType}");
        playerInventory.AddResources(resourceType, quantity);
        Debug.Log($"Now have {playerInventory.GetResourceQuantity(resourceType)} {resourceType}");
        Destroy(gameObject);
    }
}
