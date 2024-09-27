using UnityEngine;

public class ItemGem : MonoBehaviour, InventoryItem 
{
    public string Name => "Gem of Revealing";

    [SerializeField]
    private Sprite gemSprite;
    public Sprite Image => gemSprite;

    public RevealAbility reveal; // Reference to the RevealAbility script

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        // Code to use the item
        Debug.Log($"{Name} used");

        // Trigger the RevealAbility functionality if the script is assigned
        if (reveal != null)
        {
            reveal.ToggleReveal();
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