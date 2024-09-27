using UnityEngine;

public class ItemFlower : MonoBehaviour, InventoryItem 
{
    public string Name => "Elderflower Essence";

    [SerializeField]
    private Sprite featherSprite;
    public Sprite Image => featherSprite;

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
