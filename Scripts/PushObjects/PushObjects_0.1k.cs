using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 5f;  // Force applied to the object
    public float pushRadius = 0.5f;  // Radius around the player for detecting pushable objects
    public LayerMask pushableLayer;  // Layer to filter out non-pushable objects

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PushObjects();
    }

    void PushObjects()
    {
        // Detect objects within a certain radius around the player
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius, pushableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.attachedRigidbody;

            // Check if the object has a Rigidbody and is pushable
            if (rb != null && !rb.isKinematic)
            {
                // Calculate direction to push the object
                Vector3 pushDirection = (hitCollider.transform.position - transform.position).normalized;

                // Apply a force to the object in the direction of the player's movement
                Vector3 force = pushDirection * pushForce;
                rb.AddForce(force);
            }
        }
    }
}
