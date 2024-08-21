using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] Vector2 mouseSensitivity;
    [SerializeField] GameObject cameraObject;
    [SerializeField] Vector2 cameraBounds;
    [SerializeField] float movmentSpeed;
    [SerializeField] float jumpForce;

    Rigidbody rb;

    private bool onGround;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateCharacter();
        MoveRB();
    }

    private void FixedUpdate()
    {
        CheckForSlope();
        Jump();
    }



    private void RotateCharacter()
    {
        var mouseDelta = Vector2.zero;
        mouseDelta.x += Input.GetAxis("Mouse X");
        mouseDelta.y += -Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity.x);

        cameraObject.transform.Rotate(Vector3.right * mouseDelta.y * mouseSensitivity.y);

        var cameraRotation = (cameraObject.transform.localEulerAngles.x + 180) % 360;
        cameraObject.transform.localEulerAngles = new Vector3(Mathf.Min(Mathf.Max(cameraRotation - 180, cameraBounds.x), cameraBounds.y),0, 0);
    }

    private void MoveRB()
    {

        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector -= Vector3.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= Vector3.forward;
        }

        moveVector = moveVector.normalized;
        rb.angularVelocity = Vector3.zero;

        if (moveVector == Vector3.zero)
        {
            var gravityVelocity = rb.velocity.y;
            rb.velocity = rb.velocity * 0.9f;
            rb.velocity = new Vector3(rb.velocity.x, gravityVelocity, rb.velocity.z);
        }
        else
        {
            rb.AddForce(moveVector * movmentSpeed, ForceMode.VelocityChange);
        }
    }

    private void Jump()
    {
        if (!onGround) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void CheckForSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.3f) && rb.velocity.y <= 0)
        {
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity, hit.normal).normalized * rb.velocity.magnitude;
            onGround = true;
        }
    }

}
