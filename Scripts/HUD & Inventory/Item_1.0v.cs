using UnityEngine;

public class Item : MonoBehaviour, InventoryItem 
{
    public string Name 
    {
        get { return "Item"; }
    }

    public Sprite Image
    {
        get { return _Image; }
    }

    public Sprite _Image;

    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f; 
    public float rotationSpeed = 50f; 

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        FloatAndRotate();
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Debug.Log($"Picked up {name}");
    }
    
    public void Use()
    {
        // Code to use the item
        Debug.Log($"{Name} used");
    }

    private void FloatAndRotate()
    {
        // Floating logic
        Vector3 tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPosition;

        // Rotating logic
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}