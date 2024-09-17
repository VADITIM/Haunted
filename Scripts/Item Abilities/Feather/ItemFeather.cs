using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFeather : MonoBehaviour, InventoryItem 
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

    public PlayerChange playerChangeScript; 

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        Debug.Log($"{Name} used");

        if (playerChangeScript != null)
        {
            playerChangeScript.Change();
        }
    }
}
