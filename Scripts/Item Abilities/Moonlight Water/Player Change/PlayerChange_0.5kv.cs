using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public GameObject unchangedModel;
    public GameObject changedModel;

    public bool isChanged = false;  

    public PickThrow pickThrowScript;

    public Transform raycastOrigin;  // Point from which the raycast originates (e.g., the player's camera or head)
    private float raycastDistance = 1.5f;  // Maximum distance for the raycast
    public LayerMask raycastMask;  // Optional layer mask to filter what the raycast can hit

    void Start()
    {
        SetActiveModel(unchangedModel, true);
        SetActiveModel(changedModel, false);
    }

    void Update()
    {
        SynchronizeModels();

        // Perform the raycast
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.up, out RaycastHit hit, raycastDistance, raycastMask))
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.up * raycastDistance, Color.red);
            Debug.Log("Raycast hit: " + hit.collider.name);

            // If raycast hits something, prevent changing the model
            return;
        }
        else
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.up * raycastDistance, Color.green);
        }
    }

    public void Change()
    {
        // Perform the raycast again to prevent change if hit
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.up, out RaycastHit hit, raycastDistance, raycastMask))
        {
            Debug.Log("Change prevented due to raycast hit: " + hit.collider.name);
            return;  // Prevent changing the model if raycast hits
        }

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

    private void SetActiveModel(GameObject model, bool isActive)
    {
        model.SetActive(isActive);
    }

    private void SwitchToModel(GameObject activeModel, GameObject inactiveModel)
    {
        SetActiveModel(activeModel, true);
        SetActiveModel(inactiveModel, false);
    }

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
}
