using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Trigger wird ausgelöst, wenn ein Objekt die Zone betritt
    private void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob das kollidierende Objekt den Tag "Player" hat
        if (other.CompareTag("Player"))
        {
            Debug.Log("Der Spieler betritt die Todeszone.");  // Debugging-Ausgabe
            // Versuchen, das PlayerRespawn-Skript des Spielers zu finden
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            // Wenn das Skript gefunden wird, rufe die "Die()" Methode auf
            if (playerRespawn != null)
            {
                playerRespawn.Die();
            }
            else
            {
                Debug.LogWarning("Das PlayerRespawn-Skript wurde nicht gefunden!");  // Warnung, falls das Skript fehlt
            }
        }
    }
}