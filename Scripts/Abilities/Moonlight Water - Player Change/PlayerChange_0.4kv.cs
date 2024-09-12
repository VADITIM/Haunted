using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public GameObject unchangedModel;
    public GameObject changedModel;

    public bool isChanged = false;  

    public PickThrow pickThrowScript;

    void Start()
    {
        SetActiveModel(unchangedModel, true);
        SetActiveModel(changedModel, false);
    }

    void Update()
    {
        SynchronizeModels();

        if (Input.GetKeyDown(KeyCode.X))
        {
            isChanged = !isChanged;

            if (isChanged)
            {
                SwitchToModel(changedModel, unchangedModel);

                pickThrowScript.disableActions = true;
                pickThrowScript.disableDebugRay = true;
            }
            else
            {
                SwitchToModel(unchangedModel, changedModel);

                pickThrowScript.disableActions = false;
                pickThrowScript.disableDebugRay = false;
            }
        }
    }

    void SwitchToModel(GameObject newModel, GameObject oldModel)
    {
        newModel.transform.position = oldModel.transform.position;
        newModel.transform.rotation = oldModel.transform.rotation;

        // Enable model's visibility and collision
        SetActiveModel(newModel, true);
        SetActiveModel(oldModel, false);

        // Enable the PickThrow script for the new model and disable it for the old model
        PickThrow newPickThrow = newModel.GetComponentInChildren<PickThrow>();
        PickThrow oldPickThrow = oldModel.GetComponentInChildren<PickThrow>();

        if (newPickThrow != null) newPickThrow.enabled = true;
        if (oldPickThrow != null) oldPickThrow.enabled = false;
    }

    // This function ensures the inactive model follows the active model's movement
    void SynchronizeModels()
    {
        if (isChanged)
        {
            unchangedModel.transform.position = changedModel.transform.position;
            unchangedModel.transform.rotation = changedModel.transform.rotation;
        }
        else
        {
            changedModel.transform.position = unchangedModel.transform.position;
            changedModel.transform.rotation = unchangedModel.transform.rotation;
        }
    }

    // This function sets the visibility and collision state of a model
    void SetActiveModel(GameObject model, bool isActive)
    {
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isActive;
        }

        Collider[] colliders = model.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = isActive;
        }
    }
}
