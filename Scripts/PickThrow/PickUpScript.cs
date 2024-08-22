using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f; // Force at which the object is thrown
    public float pickUpRange = 5f; // How far the player can pickup the object
    private float rotationSensitivity = 1f; // How fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; // Object which we pick up
    private Rigidbody heldObjRb; // Rigidbody of object we pick up
    private bool canDrop = true; // Prevent throwing/dropping object when rotating
    private int LayerNumber; // Layer index

    // Define a fixed scale value
    [SerializeField] private Vector3 fixedScale = new Vector3(15f, 15f, 15f); // Adjust the scale as needed

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); // Make sure your holdLayer is named correctly
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Key to pick up
        {
            if (heldObj == null) // If not holding anything
            {
                RaycastHit hit;
                // Raycast to check if player is looking at object within pickup range
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    // Check if pickup tag is attached
                    if (hit.transform.gameObject.CompareTag("canPickUp"))
                    {
                        // Pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop)
                {
                    StopClipping(); // Prevent object from clipping through walls
                    DropObject();
                }
            }
        }
        
        if (heldObj != null) // If holding an object
        {
            MoveObject(); // Keep object position at holdPos
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop) // Mouse0 (left click) to throw
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.TryGetComponent<Rigidbody>(out Rigidbody rb)) // Check if object has Rigidbody
        {
            heldObj = pickUpObj; // Assign heldObj
            heldObjRb = rb; // Assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObj.layer = LayerNumber; // Change object layer to holdLayer
            
            // Lock the scale of the held object
            heldObj.transform.localScale = fixedScale; // Set to fixed scale

            // Parent the object to the hold position
            heldObj.transform.SetParent(holdPos, true);

            // Prevent collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        // Re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Assign back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; // Unparent object
        heldObj = null; // Clear held object
    }

    void MoveObject()
    {
        // Keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.position;

        heldObj.transform.localScale = fixedScale;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R)) // Hold R key to rotate
        {
            canDrop = false; // Prevent throwing during rotation
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            // Rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        // Same as drop function, but add force to object before clearing it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

    void StopClipping() // Function called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); // Distance from holdPos to the camera
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        // If the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            // Change object position to camera position
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); // Offset slightly downward to stop object dropping above player
        }
    }
}
