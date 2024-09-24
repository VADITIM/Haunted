using UnityEngine;

public class RevealAbility : MonoBehaviour
{
    public GameObject revealPanel; // The UI panel that overlays the screen
    public LayerMask hiddenObjectsLayer; // The layer of hidden objects

    private bool isRevealing = false; // State of the ability

    void Start()
    {
        // Make the panel invisible at the start of the scene
        if (revealPanel != null)
        {
            revealPanel.SetActive(false);
        }

        // Hide the objects at the start
        SetObjectsVisibility(false);
    }

    public void ToggleReveal()
    {
        isRevealing = !isRevealing;
        Debug.Log("ToggleReveal called. isRevealing: " + isRevealing);

        // Toggle visibility
        if (isRevealing)
        {
            EnableReveal();
        }
        else
        {
            DisableReveal();
        }
    }

    public void EnableReveal()
    {
        Debug.Log("EnableReveal called");

        // Show the panel
        if (revealPanel != null)
        {
            revealPanel.SetActive(true);
        }

        // Additional logic to reveal hidden objects
        SetObjectsVisibility(true);
    }

    public void DisableReveal()
    {
        Debug.Log("DisableReveal called");

        // Hide the panel
        if (revealPanel != null)
        {
            revealPanel.SetActive(false);
        }

        // Additional logic to hide hidden objects
        SetObjectsVisibility(false);
    }
    void SetObjectsVisibility(bool isVisible)
    {
        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("hiddenObject");

        foreach (GameObject obj in hiddenObjects)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            Collider objCollider = obj.GetComponent<Collider>();
            Rigidbody objRigidbody = obj.GetComponent<Rigidbody>();

            if (objRenderer != null)
            {
                objRenderer.enabled = isVisible;
            }
        }
    }

}