using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            instance = this;  // Assign the instance
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
