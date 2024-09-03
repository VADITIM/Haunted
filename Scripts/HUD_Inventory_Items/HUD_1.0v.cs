using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    private int selectedSlot = -1; // Set to -1 initially to indicate no slot is selected

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
        if (selectedSlot == slotIndex)
        {
            // If the slot is already selected, unselect it
            UnselectSlot();
        }
        else
        {
            // If a different slot is selected, select the new slot
            SelectSlot(slotIndex);
        }
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
                }
                else
                {
                    // Keep the slot fully visible (white color) if it has an item, otherwise transparent
                    slotImage.color = slotImage.sprite != null ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    private void UnselectSlot()
    {
        selectedSlot = -1;

        // Remove highlight but keep the item's sprite in the slots
        Transform inventoryPanel = transform.Find("Inventory");
        int slotCount = inventoryPanel.childCount;
        for (int i = 0; i < slotCount; i++)
        {
            Transform slot = inventoryPanel.GetChild(i);
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                // Set the slot to be fully visible if it has an item, otherwise transparent
                slotImage.color = slotImage.sprite != null ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
            }
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
        // Get the item in the selected slot and "use" it
        if (selectedSlot >= 0 && selectedSlot < Inventory.GetItemCount())
        {
            InventoryItem item = Inventory.GetItem(selectedSlot);

            if (item != null)
            {
                // Call the "use" method on the item
                item.Use();
            }
        }
    }
}
