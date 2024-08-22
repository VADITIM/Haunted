using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;
    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable") // if looking at an Interactable object.
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                // if there is a currentInteractable and it is not the newInteractable
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisabledOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // if nothing in reach
                {
                    DisableCurrentInteractable();
                }
            }
            else // if nothing in reach
            {
                DisableCurrentInteractable();
            }
        }
        else // if nothing in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(newInteractable);
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }
    
    void DisableCurrentInteractable()
    {
        if (currentInteractable)
        {
            CurrentInteractable.DisableOutline();
            CurrentInteractable = null; 
        }
    }
}
