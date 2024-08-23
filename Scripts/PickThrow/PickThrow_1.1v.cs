using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PickThrow : MonoBehaviour
{
    public GameObject Player;
    public Transform holdPosition;

    public float throwForce = 500f;
    public float pickUpRange = 3f;
    
    private GameObject heldObject;
    private Rigidbody heldObjectRB;
    private Vector3 originalScale;

    private bool canDrop = true;
    private int LayerNumber; // Layer Index

    void Start()
    {
        // make sure holdLayer exists -- important for StopClipping
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }


    void Update() 
    {
        HandleActions();
        DebugRay();
    }

    // display a ray for debugging issues 
    void DebugRay() 
    {
        // define direction
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        // if an object is infront the ray
        if (Physics.Raycast(transform.position, direction, out hit, pickUpRange)) 
        {
            // check for tag, draw green, else red
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

    // function to handle Pick Up, Drop and Throw
    void HandleActions() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldObject == null) // If not holding anything
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

    // move the picked up object to holdPosition in Unity
    void Move() 
    {
        // adjust the position of the object to holdPosition
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = holdPosition.rotation;

        // reset the scale to original scale
        heldObject.transform.localScale = originalScale;
    }

    // display the picked up object ALWAYS before you
    void StopClipping() 
    {
        // distance from holdPos and pickUpCamera
        var clipRange = Vector3.Distance(heldObject.transform.position, transform.position);
        
        // check for how many objects are within the Raycast
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