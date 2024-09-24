using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    public PickThrow pickThrow;

    private int selectedSlot = -1; 

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
    }

    private void Update()
    {
        Inputs();
        HandleItemUse();
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArguments e)
    {
        // Update the inventory UI when an item is added
        Transform inventoryPanel = transform.Find("Inventory");

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            Transform slot = inventoryPanel.GetChild(i);
            Image slotImage = slot.GetComponent<Image>();

            // Check if the slot is empty by seeing if the sprite is null
            if (slotImage != null && slotImage.sprite == null) 
            {
                // Set the item image in the empty slot
                slotImage.sprite = e.Item.Image;
                slotImage.color = new Color(1f, 1f, 1f, 1f); // Set the slot to be fully visible
                break;
            }
        }
    }

    private void Inputs()
    {
        // Detect keypresses for 1-5 and toggle selection of the slot
        if (Input.GetKeyDown(KeyCode.Alpha1)) ToggleSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ToggleSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ToggleSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ToggleSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ToggleSlot(4);
    }

    private void ToggleSlot(int slotIndex)
    {
        // Check if the slot has an item
        if (HasItemInSlot(slotIndex))
        {
            if (selectedSlot != slotIndex)
            {
                UnselectSlot(); // Unselect the currently selected slot
                SelectSlot(slotIndex); // Select the new slot
            }
            else
            {
                UnselectSlot(); // Unselect if clicking the already selected slot
            }
        }
    }

    private bool HasItemInSlot(int slotIndex)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        if (slotIndex >= 0 && slotIndex < inventoryPanel.childCount)
        {
            Transform slot = inventoryPanel.GetChild(slotIndex);
            Image slotImage = slot.GetComponent<Image>();
            return slotImage != null && slotImage.sprite != null;
        }
        return false;
    }

    private void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;

        // Highlight the selected slot
        Transform inventoryPanel = transform.Find("Inventory");
        int slotCount = inventoryPanel.childCount;
        for (int i = 0; i < slotCount; i++)
        {
            Transform slot = inventoryPanel.GetChild(i);
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                if (i == selectedSlot)
                {
                    // Highlight the selected slot with a yellow color
                    slotImage.color = new Color(1f, 1f, 0f, 1f);
                    pickThrow.Drop();
                }
                else
                {
                    // Set the color to white if it contains an item, otherwise make it invisible
                    slotImage.color = HasItemInSlot(i) ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    private void UnselectSlot()
    {
        if (selectedSlot >= 0)
        {
            Transform inventoryPanel = transform.Find("Inventory");
            int slotCount = inventoryPanel.childCount;
            for (int i = 0; i < slotCount; i++)
            {
                Transform slot = inventoryPanel.GetChild(i);
                Image slotImage = slot.GetComponent<Image>();
                if (slotImage != null)
                {
                    // Ensure the item image remains visible if it contains an item
                    slotImage.color = HasItemInSlot(i) ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
                }
            }
            selectedSlot = -1; // Reset selected slot
        }
    }

    void HandleItemUse()
    {
        if (Input.GetMouseButtonDown(0) && selectedSlot >= 0) 
        {
            Use();
        }
    }

    private void Use()
    {
        if (selectedSlot >= 0 && selectedSlot < Inventory.GetItemCount())
        {
            InventoryItem item = Inventory.GetItem(selectedSlot);

            if (item != null)
            {
                // If holding an object, drop it before using the inventory item
                if (pickThrow != null && pickThrow.IsHoldingObject())
                {
                    pickThrow.Drop();
                }
                item.Use();
            }
        }
    }

    public bool IsSlotSelected()
    {
        return selectedSlot >= 0;
    }
}
