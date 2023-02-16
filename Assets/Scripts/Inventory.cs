using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> inventoryList = new List<Items>();
    public int MaxWeight;
    public int currentWeight;
    public Inventory Singleton { get; private set; }

    public bool InventoryLock;


    private void OnEnable()
    {
        PlayerController.InventoryUpdate += UpdateInventory;
        currentWeight = 0;
        InventoryLock = false;

        if(Singleton != null)
        {
            return;
        }
    }
    private void OnDisable()
    {
        PlayerController.InventoryUpdate -= UpdateInventory;
    }

    public void UpdateInventory(Items item)
    {
        if (!InventoryLock)
        {
            inventoryList.Add(item);
            currentWeight += item.SO.itemWeigth;
            item.gameObject.SetActive(false);

            if (currentWeight > MaxWeight)
            {

                InventoryLock = true;
            }
        }
        if (InventoryLock)
        {
            Debug.Log("TOO MUCH WEIGHT");
        }
    }

}


