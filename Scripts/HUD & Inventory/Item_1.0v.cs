using UnityEngine;

public class Item : MonoBehaviour, InventoryItem 
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
        get {return _Image;}
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    
    public void Use()
    {
        // Code to use the item
        Debug.Log($"{Name} used");
    }
}
