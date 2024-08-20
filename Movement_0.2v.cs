using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSController : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] Vector2 mouseSensitivity;
    [SerializeField] GameObject cameraObject;
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
        Camera();
        MoveRB();
    }

    private void FixedUpdate()
    {
        CheckForSlope();
        Jump();
    }

    private void Camera()
    {
        var mouseDelta = Vector2.zero;
        mouseDelta.x += Input.GetAxis("Mouse X");
        mouseDelta.y += -Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity.x);

        cameraObject.transform.Rotate(Vector3.right * mouseDelta.y * mouseSensitivity.y);

        // set max and min rotation
        float maxRotationX = -90f;
        float minRotationX = 90f;

        // allow to rotate the camera in x axis
        var cameraRotationX = cameraObject.transform.localEulerAngles.x;

        if (cameraRotationX > 180f)
        {
            cameraRotationX -= 360f;
        }

        // clamp the rotation
        cameraRotationX = Mathf.Clamp(cameraRotationX, maxRotationX, minRotationX);

        // apply the clamped rotation
        cameraObject.transform.localEulerAngles = new Vector3(cameraRotationX, 0, 0);
    }
    public enum Movement
    {
        Forward,
        Backward,
        Left,
        Right,
        None
    }

    private Movement GetMoveInput()
    {
        if (Input.GetKey(KeyCode.W)) return Movement.Forward;
        if (Input.GetKey(KeyCode.S)) return Movement.Backward;
        if (Input.GetKey(KeyCode.A)) return Movement.Left;
        if (Input.GetKey(KeyCode.D)) return Movement.Right;
        return Movement.None;
    }

    private void MoveRB()
    {

        Movement movement = GetMoveInput();
        Vector3 moveVector = Vector3.zero;

        switch (movement)
        {
            case Movement.Forward:
                moveVector = Vector3.forward;
               break;
            case Movement.Backward:
                moveVector = Vector3.back;
                break;
            case Movement.Left:
                moveVector = Vector3.left;
                break;
            case Movement.Right:
                moveVector = Vector3.right;
                break;
            case Movement.None:
                break;
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
