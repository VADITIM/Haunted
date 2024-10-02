using UnityEngine;
using System.Collections;

public class PlayerDeadly : MonoBehaviour
{
    private PlayerSpawn playerSpawn;
    public float respawnDelay = 1f; 

    void Start()
    {
        playerSpawn = GetComponent<PlayerSpawn>();
        if (playerSpawn == null)
        {
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("deadly"))
        {
            HandleDeath(other);
        }
    }

    private void HandleDeath(Collider deadlyCollider)
    {
        if (playerSpawn != null)
        {
            playerSpawn.Respawn();

            StartCoroutine(DisableColliderTemporarily(deadlyCollider));
        }
    }

    private IEnumerator DisableColliderTemporarily(Collider deadlyCollider)
    {
        deadlyCollider.enabled = false;
        yield return new WaitForSeconds(respawnDelay);  // Wait for the respawn delay
        deadlyCollider.enabled = true;
    }
}
