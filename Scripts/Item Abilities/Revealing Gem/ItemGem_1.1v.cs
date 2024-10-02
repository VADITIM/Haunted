using UnityEngine;

public class ItemGem : MonoBehaviour, InventoryItem 
{
    public string Name => "Gem of Revealing";

    [SerializeField]
    private Sprite gemSprite;
    public Sprite Image => gemSprite;

    public RevealAbility reveal; 

    private float floatAmplitude = .1f;
    private float floatFrequency = 1f;

    private Vector3 startPosition;

    void Start()
     {
        startPosition = transform.position;
     }

     void Update()
     {
        Float();
     }

    private void Float()
    {
        Vector3 tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPosition;
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        if (reveal != null)
        {
            reveal.ToggleReveal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemMerger itemMerger = other.GetComponent<ItemMerger>();
        if (itemMerger != null)
        {
            itemMerger.PlaceItem(gameObject);
        }
    }
}