using UnityEngine;

public class ItemMoonlightWater : MonoBehaviour, InventoryItem 
{
    public string Name => "Moonlight Water";

    [SerializeField]
    private Sprite moonlightWaterSprite;
    public Sprite Image => moonlightWaterSprite;

    public PlayerChange playerChangeScript; // Reference to the PlayerChange script

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
        if (playerChangeScript != null)
        {
            playerChangeScript.Change();
        }
    }
}
