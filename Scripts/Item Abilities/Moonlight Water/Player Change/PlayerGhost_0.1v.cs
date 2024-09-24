using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    public GameObject unchangedModel; // The default model
    public GameObject changedModel;   // The changed model

    public bool isChanged = false;    // Keeps track of the active player model

    void Start()
    {
        // At the start, the unchanged model is visible and has collision
        SetActiveModel(unchangedModel, true);
        
        // The changed model is invisible and has no collision
        SetActiveModel(changedModel, false);
    }

    void Update()
    {
        // Switch player models when pressing the "X" key
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Toggle between the unchanged and changed model
            isChanged = !isChanged;
            
            // Apply the toggle based on the current state
            if (isChanged)
            {
                // Make changed model visible and with collision, and unchanged model invisible without collision
                SetActiveModel(changedModel, true);
                SetActiveModel(unchangedModel, false);
            }
            else
            {
                // Make unchanged model visible and with collision, and changed model invisible without collision
                SetActiveModel(unchangedModel, true);
                SetActiveModel(changedModel, false);
            }
        }
    }

    // This function sets the visibility and collision state of the model
    void SetActiveModel(GameObject model, bool isActive)
    {
        // Enable/Disable Renderer to control visibility
        Renderer renderer = model.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isActive;
        }

        // Enable/Disable Collider to control collision
        Collider collider = model.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = isActive;
        }
    }
}
