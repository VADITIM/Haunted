using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;  // Speichert die Position des letzten erreichten Checkpoints
    public TMP_Text warningText;  // UI-Textfeld für die Nachricht
    public float messageDuration = 3f;  // Dauer, wie lange die Nachricht angezeigt wird

    private Rigidbody playerRigidbody;  // Referenz auf den Rigidbody des Spielers
    private Collider playerCollider;  // Referenz auf den Collider des Spielers
    private PlayerInput playerInput;  // Skript oder Komponente für die Eingaben (z.B. Keyboard)

    private void Start()
    {
        currentCheckpoint = transform.position;  // Setze dies auf die Position des Spielers
        warningText.gameObject.SetActive(false);  // Versteckt das Textfeld zu Beginn

        // Rigidbody, Collider und Eingaben-Referenzen speichern
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerInput = GetComponent<PlayerInput>();  // Annahme: Es gibt ein Skript für die Eingaben

        // Debugging-Überprüfungen
        if (warningText == null)
        {
            Debug.LogError("Warning Text ist nicht zugewiesen! Bitte weise das Textfeld im Inspektor zu.");
        }

        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody wurde nicht gefunden! Bitte füge einen Rigidbody zum Spieler hinzu.");
        }

        if (playerCollider == null)
        {
            Debug.LogError("Collider wurde nicht gefunden! Bitte füge einen Collider zum Spieler hinzu.");
        }

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput wurde nicht gefunden! Eingaben können nicht deaktiviert werden.");
        }
    }

    // Setzt den aktuellen Checkpoint, wenn der Spieler einen neuen Checkpoint erreicht
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;  // Speichert die Position des neuen Checkpoints
        Debug.Log("Neuer Checkpoint gesetzt: " + currentCheckpoint);  // Debugging-Ausgabe
    }

    // Methode, um den Tod des Spielers zu behandeln
    public void Die()
    {
        Debug.Log("Der Spieler stirbt und wird respawnen an: " + currentCheckpoint);  // Debugging-Ausgabe

        // Eingabe blockieren
        if (playerInput != null)
        {
            playerInput.enabled = false;  // Deaktiviert die Eingaben
            Debug.Log("Spielereingaben deaktiviert.");
        }

        // Physik deaktivieren
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = true;  // Deaktiviert die Physik
            Debug.Log("Physik des Spielers deaktiviert.");
        }

        // Collider deaktivieren
        if (playerCollider != null)
        {
            playerCollider.enabled = false;  // Deaktiviert den Collider
            Debug.Log("Collider des Spielers deaktiviert.");
        }

        // Spieler wird zum letzten Checkpoint teleportiert
        transform.position = currentCheckpoint;

        // Reaktiviere die Physik und Eingaben nach einer kurzen Verzögerung
        StartCoroutine(ReenableComponents());

        // Startet eine Coroutine, um die Warnmeldung anzuzeigen
        StartCoroutine(ShowWarningMessage());
    }

    // Coroutine, um die Physik, den Collider und die Eingaben nach einem Frame wieder zu aktivieren
    private IEnumerator ReenableComponents()
    {
        yield return new WaitForFixedUpdate();  // Wartet bis zum nächsten Physik-Update

        // Physik wieder aktivieren
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;  // Reaktiviert die Physik
            Debug.Log("Physik des Spielers wieder aktiviert.");
        }

        // Collider wieder aktivieren
        if (playerCollider != null)
        {
            playerCollider.enabled = true;  // Reaktiviert den Collider
            Debug.Log("Collider des Spielers wieder aktiviert.");
        }

        // Eingaben wieder aktivieren
        if (playerInput != null)
        {
            playerInput.enabled = true;  // Reaktiviert die Eingaben
            Debug.Log("Spielereingaben wieder aktiviert.");
        }
    }

    // Coroutine, um die Warnmeldung für eine bestimmte Zeit anzuzeigen
    private IEnumerator ShowWarningMessage()
    {
        warningText.text = "Pass besser auf!";  // Setzt die Warnmeldung
        warningText.gameObject.SetActive(true);  // Blendet das Textfeld ein
        yield return new WaitForSeconds(messageDuration);  // Wartet für die angegebene Dauer
        warningText.gameObject.SetActive(false);  // Blendet das Textfeld wieder aus
    }
}