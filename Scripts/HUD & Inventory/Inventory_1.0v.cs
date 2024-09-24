using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private const int SLOTS = 5;

    private List<InventoryItem> maxItems = new List <InventoryItem>();

    public event EventHandler<InventoryEventArguments> ItemAdded;

    public void AddItem(InventoryItem item)
    {
        if (maxItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                collider.enabled = false;
                maxItems.Add(item);
                item.OnPickUp();

                if (ItemAdded != null) 
                {
                    ItemAdded(this, new InventoryEventArguments(item));
                }
            }
        }
    }


    public InventoryItem GetItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < maxItems.Count)
        {
            return maxItems[slotIndex];
        }
        return null;
    }

    public int GetItemCount()
    {
        return maxItems.Count;
    }

    
}
