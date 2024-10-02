using UnityEngine;

public class ItemHorn : MonoBehaviour, InventoryItem 
{
    public string Name => "Unicorn Horn";

    [SerializeField]
    private Sprite unicornHornSprite;
    public Sprite Image => unicornHornSprite;

    public HornAbility hornAbilityScript; 
    public Light externalLight; 
    public Transform playerTransform; 

    private bool isLightActive = false; 

    public Vector3 teleportPosition; 

    private float floatAmplitude = .1f;
    private float floatFrequency = 1f;

    private Vector3 startPosition;

    void Start()
     {
        startPosition = transform.position;
     }

    private void Float()
    {
        Vector3 tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPosition;
    }

    public void OnPickUp()
    {
        Debug.Log($"Picked up {Name}");
    }

    public void Use()
    {
        if (hornAbilityScript != null)
        {
            hornAbilityScript.ToggleLight(externalLight); 
            isLightActive = !isLightActive; 
        }
        else
        {
            Debug.LogError("HornAbility script is not assigned.");
        }
    }

    void Update()
    {
        Float();
        if (isLightActive && externalLight != null && playerTransform != null)
        {
            externalLight.transform.position = playerTransform.position; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startPosition = teleportPosition;
        }
    }
}