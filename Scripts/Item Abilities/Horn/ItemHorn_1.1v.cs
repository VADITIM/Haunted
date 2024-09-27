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

    public void OnPickUp()
    {
        Debug.Log($"Picked up {Name}");
    }

    public void Use()
    {
        Debug.Log($"{Name} used");

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
        if (isLightActive && externalLight != null && playerTransform != null)
        {
            externalLight.transform.position = playerTransform.position; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = teleportPosition;
        }
    }
}