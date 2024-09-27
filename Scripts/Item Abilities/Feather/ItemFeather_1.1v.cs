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

    public FeatherAbility FeatherAbilityScript; 

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        Debug.Log($"{Name} used");

        if (FeatherAbilityScript != null)
        {
            FeatherAbilityScript.CreateDestroyArea();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the ItemMerger
        ItemMerger itemMerger = other.GetComponent<ItemMerger>();
        if (itemMerger != null)
        {
            itemMerger.PlaceItem(gameObject);
        }
    }
}
