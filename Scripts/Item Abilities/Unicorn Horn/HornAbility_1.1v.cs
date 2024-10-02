using UnityEngine;

public class HornAbility : MonoBehaviour
{
    public Transform raycastOrigin;  
    private float rayDistance = 5f;  

    public GameObject objectToSpawn; 
    public GameObject dialogueCollider; 
    private int litTorchCount = 0;
    private int requiredLitTorches = 5;

    private bool isLightOn = false;

    public AudioClip toggleSound; // Sound to play when the chest is toggled

    private AudioSource audioSource; // AudioSource component


    void Start()
    {
        objectToSpawn.SetActive(false);
        dialogueCollider.SetActive(false);
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void TorchLit()
    {
        litTorchCount++;
        if (litTorchCount >= requiredLitTorches)
        {
            objectToSpawn.SetActive(true);
            dialogueCollider.SetActive(true);
            if (requiredLitTorches == 5)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }
    }
    
    public void ToggleLight(Light externalLight)
    {
        if (externalLight != null)
        {
            Vector3 rayOrigin = raycastOrigin.position;
            Vector3 rayDirection = raycastOrigin.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.CompareTag("lightable") && isLightOn)
                {
                    return;
                }

                if (hit.collider.CompareTag("torch") && isLightOn)
                {
                    TorchLit();
                    return;
                }
            }
            externalLight.enabled = !isLightOn;
            isLightOn = !isLightOn;
        }
    }

    void Update()
    {
        Vector3 rayOrigin = raycastOrigin.position;
        Vector3 rayDirection = raycastOrigin.forward;


        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("lightable") || hit.collider.CompareTag("torch"))
            {
                if (Input.GetMouseButtonDown(0) && isLightOn)
                {
                    GameObject lightableObject = hit.collider.gameObject;
                    Light pointLight = lightableObject.AddComponent<Light>();
                    pointLight.type = LightType.Point;
                    pointLight.range = 10.0f; 
                    pointLight.intensity = 1.0f; 
                    pointLight.color = Color.yellow;
                }
            }
        }
    }
}