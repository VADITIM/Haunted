using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public GameObject unchangedModel;
    public GameObject changedModel;

    public bool isChanged = false;  

    public PickThrow pickThrowScript;

    public Transform raycastOrigin; 
    private float raycastDistance = 1.5f;  
    public LayerMask raycastMask; 

    void Start()
    {
        SetActiveModel(unchangedModel, true);
        SetActiveModel(changedModel, false);
    }

    void Update()
    {
        SynchronizeModels();

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.up, out RaycastHit hit, raycastDistance, raycastMask))
        {

            return;
        }
        else
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.up * raycastDistance, Color.green);
        }
    }

    public void Change()
    {
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.up, out RaycastHit hit, raycastDistance, raycastMask))
        {
            return; 
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
