using UnityEngine;

public class ItemGem : MonoBehaviour, InventoryItem 
{
    public string Name 
    {
        get {
            return "Gem";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }

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
}