using UnityEngine;

public class HornAbility : MonoBehaviour
{
    public Transform raycastOrigin;  
    private float rayDistance = 5f;  

    private bool isLightOn = false;

    public void ToggleLight(Light externalLight)
    {
        if (externalLight != null)
        {
            // Perform the raycast to check if it hits a "lightable" object
            Vector3 rayOrigin = raycastOrigin.position;
            Vector3 rayDirection = raycastOrigin.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
            {
                // If the raycast hits a "lightable" object and the light is on, prevent deactivation
                if (hit.collider.CompareTag("lightable") && isLightOn)
                {
                    Debug.Log("Cannot deactivate the light while hitting a lightable object.");
                    return;
                }
            }

            // Toggle the light state
            externalLight.enabled = !isLightOn;
            isLightOn = !isLightOn;
        }
        else
        {
            Debug.LogError("External light is not assigned.");
        }
    }

    void Update()
    {
        Vector3 rayOrigin = raycastOrigin.position;
        Vector3 rayDirection = raycastOrigin.forward;

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.blue);

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
        {
            // Check if the hit object has the tag "lightable"
            if (hit.collider.CompareTag("lightable"))
            {
                // Check if mouse button 1 is pressed and the light is on
                if (Input.GetMouseButtonDown(0) && isLightOn)
                {
                    // Assign a point light to the hit object
                    GameObject lightableObject = hit.collider.gameObject;
                    Light pointLight = lightableObject.AddComponent<Light>();
                    pointLight.type = LightType.Point;
                    pointLight.range = 10.0f; // Adjust the range as needed
                    pointLight.intensity = 1.0f; // Adjust the intensity as needed
                }
            }
        }
    }
}