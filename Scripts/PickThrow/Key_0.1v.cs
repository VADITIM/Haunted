// Assets/Scripts/KeyScript.cs
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 doorOpenPosition; // The position to move the door to when opened

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            // Move the door to the specified position
            collision.gameObject.transform.position += doorOpenPosition;
            // Optionally, you can destroy the key after use
            Destroy(gameObject);
        }
    }
}