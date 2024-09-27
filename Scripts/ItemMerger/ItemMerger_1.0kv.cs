using UnityEngine;

public class ItemMerger : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject mergedItem;

    private bool[] itemsPlaced = new bool[5];

    void Start()
    {
        mergedItem.SetActive(false);
    }

    void Update()
    {
        if (AllItemsPlaced())
        {
            MergeItems();
        }
    }

    private bool AllItemsPlaced()
    {
        foreach (bool placed in itemsPlaced)
        {
            if (!placed)
            {
                return false;
            }
        }
        return true;
    }

    private void MergeItems()
    {
        // Schalte das Merged Item ein
        mergedItem.SetActive(true);

        // Schalte die Items aus
        Destroy(item1);
        Destroy(item2);
        Destroy(item3);
        Destroy(item4);
        Destroy(item5);
    }

    public void PlaceItem(GameObject item)
    {
        if (item == item1)
        {
            itemsPlaced[0] = true;
        }
        else if (item == item2)
        {
            itemsPlaced[1] = true;
        }
        else if (item == item3)
        {
            itemsPlaced[2] = true;
        }
        else if (item == item4)
        {
            itemsPlaced[3] = true;
        }
        else if (item == item5)
        {
            itemsPlaced[4] = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the other object has the tag "Item"
        if (other.CompareTag("item"))
        {
            PlaceItem(other.gameObject);
        }
    }
}