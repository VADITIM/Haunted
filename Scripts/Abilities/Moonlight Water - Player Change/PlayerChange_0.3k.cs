using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public GameObject unchangedModel; // The default model
    public GameObject changedModel;   // The changed model

    public bool isChanged = false;    // Keeps track of which player model is active

    void Start()
    {
        // On start, the unchanged model is visible and has collision
        SetActiveModel(unchangedModel, true);
        // The changed model is invisible and without collision
        SetActiveModel(changedModel, false);
    }

    void Update()
    {
        // Synchronize the inactive model to follow the active model's position and rotation
        SynchronizeModels();

        // Switch player models when pressing the "X" key
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Toggle between the unchanged and changed model
            isChanged = !isChanged;

            // Apply the toggle based on the current state
            if (isChanged)
            {
                // Make changed model visible and with collision, and unchanged model invisible without collision
                SwitchToModel(changedModel, unchangedModel);
            }
            else
            {
                // Make unchanged model visible and with collision, and changed model invisible without collision
                SwitchToModel(unchangedModel, changedModel);
            }
        }
    }

    // Switch to a new model, setting its position to match the currently active one
    void SwitchToModel(GameObject newModel, GameObject oldModel)
    {
        // Set the new model's position and rotation to match the old model's position and rotation
        newModel.transform.position = oldModel.transform.position;
        newModel.transform.rotation = oldModel.transform.rotation;

        // Enable the new model's visibility and collision
        SetActiveModel(newModel, true);
        // Disable the old model's visibility and collision
        SetActiveModel(oldModel, false);
    }

    // This function ensures the inactive model follows the active model's movement
    void SynchronizeModels()
    {
        if (isChanged)
        {
            // If changed model is active, the unchanged model should follow it
            unchangedModel.transform.position = changedModel.transform.position;
            unchangedModel.transform.rotation = changedModel.transform.rotation;
        }
        else
        {
            // If unchanged model is active, the changed model should follow it
            changedModel.transform.position = unchangedModel.transform.position;
            changedModel.transform.rotation = unchangedModel.transform.rotation;
        }
    }

    // This function sets the visibility and collision state of a model
    void SetActiveModel(GameObject model, bool isActive)
    {
        // Enable/Disable Renderer to control visibility
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isActive;
        }

        // Enable/Disable Collider to control collision
        Collider[] colliders = model.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = isActive;
        }
    }
}
