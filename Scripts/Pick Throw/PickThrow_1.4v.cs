using UnityEngine;

public class PickThrow : MonoBehaviour
{
    public GameObject Player;
    public Transform holdPosition;

    public float throwForce = 500f;
    public float pickUpRange = 3f;
    
    private GameObject holdingObject;
    private Rigidbody heldObjectRB;
    private Vector3 originalScale;

    public bool canPickUp = true;
    public bool canDrop = true;
    private int LayerNumber;


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

        HandleActions();
    }


    void HandleActions() 
    {
        if (hud == null)
        {
            return;  
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (holdingObject == null)
            {
                // Check if a slot is selected in the HUD
                if (!hud.IsSlotSelected()) // Prevent pickup if a slot is selected
                {
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
            originalScale = holdingObject.transform.localScale;    
            heldObjectRB.isKinematic = true;
            
            holdingObject.layer = LayerNumber;     

            Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), true);
        }
    }

    void Throw()
    {
        Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        holdingObject.layer = 0;
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * throwForce);

        holdingObject = null;
    }
    

    public void Drop() 
    {
        Physics.IgnoreCollision(holdingObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        holdingObject.layer = 0;
        heldObjectRB.isKinematic = false; 
        
        holdingObject = null;
    }

    void MoveToObjectPosition() 
    {
        holdingObject.transform.position = holdPosition.position;
        holdingObject.transform.rotation = holdPosition.rotation;

        holdingObject.transform.localScale = originalScale;
    }

    void StopClipping() 
    {
        var clipRange = Vector3.Distance(holdingObject.transform.position, transform.position);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            holdingObject.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

    public bool IsHoldingObject() 
    {
        return holdingObject != null;
    }
    
}
