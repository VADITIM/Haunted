using UnityEngine;

public class ItemMoonlightWater : MonoBehaviour, InventoryItem 
{
    public string Name => "Moonlight Water";

    [SerializeField]
    private Sprite moonlightWaterSprite;
    public Sprite Image => moonlightWaterSprite;

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
