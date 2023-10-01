using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ResourceType, int> resources;

    private void Update() {
        DebugCanvas.AddDebugText("Wood", $"{resources[ResourceType.Wood]}");
        DebugCanvas.AddDebugText("Scrap", $"{resources[ResourceType.Scrap]}");
    }

    private void Awake() {
        resources = new Dictionary<ResourceType, int>();
        
        // Add every newly created resource here
        resources[ResourceType.Wood] = 0;
        resources[ResourceType.Scrap] = 0;
    }

    public void AddResources(ResourceType resourceType, int quantity = 1) {
        if (resources.ContainsKey(resourceType)) {
            resources[resourceType] += quantity;
        }
        else {
            Debug.Log($"Trying To Add Non-Existent Resource: {resourceType} and quantity {quantity}");
        }
    }

    public void RemoveResources(ResourceType resourceType, int quantity = 1) {
        if (resources.ContainsKey(resourceType)) {
            resources[resourceType] = Mathf.Max(0, resources[resourceType] - quantity);
        }
        else {
            Debug.Log($"Trying To Remove Non-Existent Resource: {resourceType} and quantity {quantity}");
        }
    }

    public int GetResourceQuantity(ResourceType resourceType) { 
        if (resources.ContainsKey(resourceType)) {
            return resources[resourceType];
        }
        else {
            Debug.Log($"Trying to get invalid resource type {resourceType}");
            return -1;
        }
    }
    
}
