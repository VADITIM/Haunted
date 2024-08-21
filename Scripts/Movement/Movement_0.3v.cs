using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSController : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] Vector2 mouseSensitivity  = new Vector2 (5f, 5f);
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

    private void FixedUpdate()
    {
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

        // Create directions for camera
        Vector3 cameraForward = cameraObject.transform.forward;
        Vector3 cameraRight = cameraObject.transform.right;

        // Ignore upward movement
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        switch (movement)
        {
            case Movement.Forward:   moveVector = cameraForward;    break;
            case Movement.Backward:  moveVector = -cameraForward;   break;
            case Movement.Left:      moveVector = -cameraRight;     break;
            case Movement.Right:     moveVector = cameraRight;      break;
            case Movement.None:                                     break;
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

    // Seperate Gravity 
    private void Gravity()
    {

    }

    private void HandleJump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            /*Debug.Log("Jump");*/
        }
    }

    // Check if player is on ground
    private bool onGround;
    private void CheckForSlope()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = -Vector3.up;
        float rayDistance = 1.2f;

        /*Debug.Log(onGround);*/

        // (Draw) Raycast to check if the player is on the ground
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
