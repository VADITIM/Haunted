using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSController : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] Vector2 mouseSensitivity = new Vector2(5f, 5f);
    [SerializeField] GameObject cameraObject;
    [SerializeField] float movmentSpeed = 0.1f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody rb;

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
        CheckForSlope();
        HandleJump();
    }

    private void Camera()
    {
        // Get the mouse move inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity.x;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity.y;

        // Rotate the player around the Y axis (horizontal)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around its local X axis (vertical)
        cameraObject.transform.Rotate(Vector3.right * -mouseY);

        // Clamp the camera's vertical rotation
        float maxRotationX = 90f;
        float minRotationX = -90f;

        // Get current X rotation of the camera
        float currentXRotation = cameraObject.transform.localEulerAngles.x;

        // Convert to a range of -180 to 180 degrees
        if (currentXRotation > 180f)
        {
            currentXRotation -= 360f;
        }

        // Clamp the rotation between min and max limits
        currentXRotation = Mathf.Clamp(currentXRotation, minRotationX, maxRotationX);

        // Apply the clamped rotation
        cameraObject.transform.localEulerAngles = new Vector3(currentXRotation, 0, 0);
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
        Movement move = GetMoveInput();
        Vector3 moveVector = Vector3.zero;

        // Create directions for camera
        Vector3 cameraForward = cameraObject.transform.forward;
        Vector3 cameraRight = cameraObject.transform.right;

        // Ignore upward move
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        switch (move)
        {
            case Movement.Forward: moveVector = cameraForward; break;
            case Movement.Backward: moveVector = -cameraForward; break;
            case Movement.Left: moveVector = -cameraRight; break;
            case Movement.Right: moveVector = cameraRight; break;
            case Movement.None: break;
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

    private void HandleJump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool onGround;
    private void CheckForSlope()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = -Vector3.up;
        float rayDistance = 1.2f;

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance) && rb.velocity.y <= 0)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
