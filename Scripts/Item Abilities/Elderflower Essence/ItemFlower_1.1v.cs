using Unity.VisualScripting;
using UnityEngine;

public class ItemFlower : MonoBehaviour, InventoryItem 
{
    public string Name => "Elderflower Essence";

    [SerializeField]
    private Sprite featherSprite;
    public Sprite Image => featherSprite;

    public FlowerAbility flowerAbilityScript; 

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
        if (flowerAbilityScript != null)
        {
            flowerAbilityScript.SpawnAndScaleObject();
        }
    }
}
