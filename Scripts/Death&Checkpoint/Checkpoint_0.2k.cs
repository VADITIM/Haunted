using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string requiredTag = "Player";  // Tag des Objekts, das den Checkpoint aktiviert

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            // Sucht das PlayerRespawn-Skript im Spielerobjekt
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                // Setzt den aktuellen Checkpoint auf die Position dieses Checkpoints
                playerRespawn.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint erreicht! Position: " + transform.position);
            }
        }
    }
}