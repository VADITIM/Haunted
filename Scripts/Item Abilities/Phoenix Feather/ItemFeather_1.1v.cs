using UnityEngine;

public class ItemFeather : MonoBehaviour, InventoryItem 
{
    public string Name 
    {
        get { return "Item";}
    }

    public Sprite _Image;

    public Sprite Image
    { 
        get { return _Image; }
    }

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

    public FeatherAbility FeatherAbilityScript; 

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }

    public void Use()
    {
        if (FeatherAbilityScript != null)
        {
            FeatherAbilityScript.CreateDestroyArea();
        }
    }
}
