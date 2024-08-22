using UnityEngine;

public class PickThrow : MonoBehaviour
{
    public GameObject Player;
    public Transform holdPosition;

    public float throwForce = 500f;
    public float pickUpRange = 5f;
    
    private GameObject heldObject;
    private Rigidbody heldObjectRB;
    private Vector3 originalScale;

    private bool canDrop = true;
    private int LayerNumber; // Layer Index

    void Start()
    {
        // make sure holdLayer exists 
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldObject == null) // If not holding anything
            {
                // Raycast to check if player is looking at obj
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.CompareTag("canPickUp"))
                    {
                        PickUp(hit.transform.gameObject);
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
        // if not holding an object
        if (heldObject != null) 
        {
            // keep objects position and rotation at holdPos
            Move();

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
            heldObject = pickUpObject;
            heldObjectRB = rb;
            originalScale = heldObject.transform.localScale;    // copy the scale of the object
            heldObjectRB.isKinematic = true;
            heldObject.layer = LayerNumber;     // change object layer to holdLayer in Unity

            // stop collision with player while held
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), true);
        }
    }

    void Throw()
    {
        // activate collision with player after throw
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        heldObject.layer = 0;
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * throwForce);

        heldObject = null;
    }
    

    void Drop() 
    {
        // activate collision with player after drop
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
        heldObject.layer = 0;
        heldObjectRB.isKinematic = false; 
        
        heldObject = null;
    }

    void Move() 
    {
        // adjust the position of the object to holdPos
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = holdPosition.rotation;

        // reset the scale to original scale
        heldObject.transform.localScale = originalScale;
    }

    void StopClipping() 
    {
        // distance from holdPos and pickUpCamera
        var clipRange = Vector3.Distance(heldObject.transform.position, transform.position);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        // if more than one object can be carried
        if (hits.Length > 1)
        {
            // change position to camera position
            heldObject.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}