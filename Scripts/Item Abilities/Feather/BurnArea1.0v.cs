using UnityEngine;

public class BurnArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the burn area trigger: " + other.name);
        
        if (other.CompareTag("burnable"))
        {
            Debug.Log("Burnable object detected: " + other.name);
            Destroy(other.gameObject);
        }
    }
}
    