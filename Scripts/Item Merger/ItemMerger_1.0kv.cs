using UnityEngine;

public class ItemMerger : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject mergedItem;
    public GameObject timeChanger;
    public GameObject bunrableObjects;
    public GameObject Flower;
    public GameObject List;
    public GameObject DialogueTrigger;

    private bool[] itemsPlaced = new bool[3];
    private bool hasMerged = false; 

    void Start()
    {
        mergedItem.SetActive(false);
        timeChanger.SetActive(false);
        bunrableObjects.SetActive(false);
        Flower.SetActive(false);
        List.SetActive(false);
        DialogueTrigger.SetActive(false);
    }

    void Update()
    {
        if (AllItemsPlaced() && !hasMerged)
        {
            MergeItems();
            hasMerged = true;
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
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);

        Vector3 newPosition = transform.position + new Vector3(0, 0.28f, 0);
        mergedItem.transform.position = newPosition;

        mergedItem.transform.position = newPosition;

        mergedItem.SetActive(true);
        bunrableObjects.SetActive(true);
        timeChanger.SetActive(true);
        Flower.SetActive(true);
        List.SetActive(true);
        DialogueTrigger.SetActive(true);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        PlaceItem(other.gameObject);
    }
}