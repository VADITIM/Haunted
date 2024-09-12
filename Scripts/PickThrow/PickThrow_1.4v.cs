using UnityEngine;

public class PickThrow : MonoBehaviour
{
    public GameObject Player;
    public Transform holdPosition;

    public float throwForce = 500f;
    public float pickUpRange = 3f;
    
    public GameObject holdingObject;
    private Rigidbody heldObjectRB;
    private Vector3 originalScale;

    public bool canPickUp = true;
    public bool canDrop = true;
    private int LayerNumber; // Layer Index


    public bool disableActions = false;
    public bool disableDebugRay = false;

    public HUD hud; 

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }


    void Update()
    {
        if (disableActions) return;

        if (hud == null)
        {
            Debug.LogError("HUD reference is missing in PickThrow script!");
            return;  
        }

        HandleActions();
        // DebugRay();
    }

    void DebugRay() 
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, pickUpRange)) 
        {
            if (hit.transform.gameObject.CompareTag("canPickUp"))
            {
                Debug.DrawRay(transform.position, direction * pickUpRange, Color.green);
            }
            else 
            {
                Debug.DrawRay(transform.position, direction * pickUpRange, Color.red);
            }
        }    
        else 
        {
            Debug.DrawRay(transform.position, direction * pickUpRange, Color.red);
        }
    }

    void HandleActions() 
    {
        // Ensure hud is assigned before proceeding
        if (hud == null)
        {
            return;  // Stop further execution if HUD is null
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (holdingObject == null)
            {
                // Check if a slot is selected in the HUD
                if (!hud.IsSlotSelected()) // Prevent pickup if a slot is selected
                {
                    // Raycast to check if player is looking at obj
                    RaycastHit hit;
                    Vector3 direction = transform.TransformDirection(Vector3.forward);

                    if (Physics.Raycast(transform.position, direction, out hit, pickUpRange))
                    {
                        if (hit.transform.gameObject.CompareTag("canPickUp"))
                        {
                            PickUp(hit.transform.gameObject);
                        }
                    }
                }
            }
            else 
            {
                if (canDrop) 
                {
                    StopClipping();
                    Drop();
                }
            }
        }
        if (holdingObject != null) 
        {
            MoveToObjectPosition();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop)
            {
                StopClipping();
                Throw();
            }
        }
    }

    void PickUp(GameObject pickUpObject) 
    {
        if (pickUpObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            holdingObject = pickUpObject;
            heldObjectRB = rb;
            originalScale = holdingObject.transform.localScale;    // copy the scale of the object
            heldObjectRB.isKinematic = true;
            
            holdingObject.layer = LayerNumber;     // change object layer to holdLayer in Unity

            // stop collision with player while held
            Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), true);
        }
    }

    void Throw()
    {
        // activate collision with player after throw
        Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        holdingObject.layer = 0;
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * throwForce);

        holdingObject = null;
    }
    

    public void Drop() 
    {
        // activate collision with player after drop
        Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        holdingObject.layer = 0;
        heldObjectRB.isKinematic = false; 
        
        holdingObject = null;
    }

    void MoveToObjectPosition() 
    {
        // adjust the position of the object to holdPosition
        holdingObject.transform.position = holdPosition.position;
        holdingObject.transform.rotation = holdPosition.rotation;

        // reset the scale to original scale
        holdingObject.transform.localScale = originalScale;
    }

    void StopClipping() 
    {
        var clipRange = Vector3.Distance(holdingObject.transform.position, transform.position);
        
        // check for how many objects are within the Raycast
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            // change position to camera position
            holdingObject.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

    public bool IsHoldingObject() 
    {
        return holdingObject != null;
    }
    
}
