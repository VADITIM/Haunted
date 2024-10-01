using UnityEngine;

public class BurnArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("burnable"))
        {
            Destroy(other.gameObject);
        }
    }
}
    