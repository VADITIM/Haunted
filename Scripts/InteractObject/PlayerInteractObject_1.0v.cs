using UnityEngine;

public class PlayerInteractObject : MonoBehaviour
{
    public LayerMask mask;
    public GameObject interactionUI; 
    public Camera cam;
    public float distance = 4f;

    void Update()
    {
        CheckForInteractable();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.CompareTag("lever") || hitInfo.collider.CompareTag("debris") || hitInfo.collider.CompareTag("chest"))
            {
                interactionUI.SetActive(true);
                return;
            }
        }
        interactionUI.SetActive(false); // Disable the text object if no relevant object is hit
    }

    private void Interact()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.CompareTag("lever"))
            {
                Lever lever = hitInfo.collider.GetComponent<Lever>();
                if (lever != null)
                {
                    lever.Interact();
                }
            }
            else if (hitInfo.collider.CompareTag("debris"))
            {
                Debris debris = hitInfo.collider.GetComponent<Debris>();
                if (debris != null)
                {
                    debris.Loot();
                }
            }
            else if (hitInfo.collider.CompareTag("chest"))
            {
                Chest chest = hitInfo.collider.GetComponent<Chest>();
                if (chest != null)
                {
                    StartCoroutine(chest.ToggleChest());
                }
            }
        }
    }
}