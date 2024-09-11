using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem 
{
    string Name {get;}
    Sprite Image {get;}
    void OnPickUp();
    void Use();
}

public class InventoryEventArguments : EventArgs
{
    public InventoryEventArguments(InventoryItem item)
    {
        Item = item;

    }
    public InventoryItem Item;
}
