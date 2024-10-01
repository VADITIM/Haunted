// Assets/Scripts/KeyScript.cs
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 doorOpenPosition; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            collision.gameObject.transform.position += doorOpenPosition;
            Destroy(gameObject);
        }
    }
}