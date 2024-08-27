using System.Collections;
using System.Collections.Generic;
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
    private bool hasMerged = false;  // Neues Flag, um das mehrfache Mergen zu verhindern

    void Start()
    {
        mergedItem.SetActive(false);
    }

    void Update()
    {
        if (AllItemsPlaced() && !hasMerged)
        {
            MergeItems();
            hasMerged = true;  // Markiert das Merging als abgeschlossen
        }
    }

    private bool AllItemsPlaced()
    {
        for (int i = 0; i < itemsPlaced.Length; i++)
        {
            if (!itemsPlaced[i])
            {
                return false;
            }
        }
        return true;
    }

    private void MergeItems()
    {
        // Verstecke die einzelnen Items
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
        item4.SetActive(false);
        item5.SetActive(false);

        // Setze die Position des mergedItem leicht über die Oberfläche des ItemMergers
        
        Vector3 newPosition = transform.position + new Vector3(0, 0.28f, 0); // 1.0f ist der Abstand nach oben, kannst du anpassen
        mergedItem.transform.position = newPosition;

        // Setze die Position des mergedItem auf die Position des ItemMergers
        mergedItem.transform.position = newPosition;

        // Schalte das Merged Item ein
        mergedItem.SetActive(true);

        Debug.Log("Items merged into: " + mergedItem.name);
    }

    public void PlaceItem(GameObject item)
    {
        Debug.Log("Item placed: " + item.name);

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

    private void OnTriggerEnter(Collider other)
    {
        PlaceItem(other.gameObject);
    }
}