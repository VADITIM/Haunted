using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string requiredTag = "SpecialObject";  // Tag des Objekts, das den Checkpoint aktiviert

    private void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob das kollidierende Objekt den gewünschten Tag hat
        if (other.CompareTag(requiredTag))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                // Setzt den aktuellen Checkpoint auf die Position dieses Checkpoints
                playerRespawn.SetCheckpoint(transform.position);  // Setzt den Checkpoint auf die Position des Checkpoints
                Debug.Log("Checkpoint erreicht! Position: " + transform.position);  // Debugging-Ausgabe
            }
            else
            {
                Debug.LogWarning("Das PlayerRespawn-Skript wurde nicht gefunden!");  // Warnung, falls das Skript fehlt
            }
        }
        else
        {
            Debug.Log("Objekt hat nicht den erforderlichen Tag: " + other.tag);  // Ausgabe, wenn der Tag nicht übereinstimmt
        }
    }
}
