using UnityEngine;

public class ItemFlower : MonoBehaviour, InventoryItem 
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

    public FlowerAbility flowerAbilityScript; // Reference to the PlayerChange script

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
        if (flowerAbilityScript != null)
        {
            flowerAbilityScript.SpawnAndScaleObject();
        }
    }
}
