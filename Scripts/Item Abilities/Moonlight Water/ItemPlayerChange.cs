using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayerChange : MonoBehaviour, InventoryItem 
{
    public string Name 
    {
        get {
            return "Item";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }

    public PlayerChange playerChangeScript; // Reference to the PlayerChange script

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        // Code to use the item
        Debug.Log($"{Name} used");

        // Trigger the PlayerChange functionality if the script is assigned
        if (playerChangeScript != null)
        {
            playerChangeScript.Change();
        }
    }
}
