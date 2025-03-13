using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    //public GameObject currentItem;

    List<InventoryItem> Inventory = new List<InventoryItem>();
    public List<Ingredients> ingredients;

    public Sprite spriteOfPressedItem;

    private void Awake()
    {
        DefineItemsToInventory();
        RandomlyGiveItems();

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


    public void DefineItemsToInventory()
    {
        foreach(Ingredients ingr in ingredients)
        {
            GameObject item = new();

            InventoryItem iitem = item.AddComponent<InventoryItem>();


            iitem.ingredient = ingr;  // Ingredients sýnýfý tanýmlý olmalý
            iitem.quantity = 0;

            Inventory.Add(iitem);

            //print("Item added to Inventory : " + iitem.ingredient.ingrName);

            //Inventory.Add(new InventoryItem { ingredient = ingr, quantity = 99 });

        }
    }
    
    public void AddItem(string ItemName, int quantity)
    {
        foreach (var data in Inventory)
        {
            if (data.ingredient.ingrName == ItemName)
            {
                data.quantity += quantity;
                return;
            }
        }
    }

    public void DelItem(string ItemName, int quantity)
    {
        foreach (var data in Inventory)
        {
            if (data.ingredient.ingrName == ItemName)
            {
                data.quantity -= quantity;
                return;
            }
        }
    }

    public void RandomlyGiveItems()
    {
        int randomNum = 0;
        for(int i=0; i<Inventory.Count; i++)
        {

            randomNum = Random.Range(0, 3);

            Inventory[i].quantity += randomNum;
        }
    }

    public InventoryItem SearchItem(string ItemName)
    {
        foreach (var data in Inventory)
        {
            if (data.ingredient.ingrName == ItemName)
            {
                return data;
            }
        }
        return null;
    }

    private void OnEnable()
    {
        DialogueManager.CustomerReceivedPotion += CustomerReceivedPotion;
    }

    private void CustomerReceivedPotion()
    {
        RandomlyGiveItems();
    }

    private void OnDisable()
    {
        DialogueManager.CustomerReceivedPotion -= CustomerReceivedPotion;
    }

}
