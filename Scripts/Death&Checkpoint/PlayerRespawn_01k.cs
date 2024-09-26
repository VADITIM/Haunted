using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Für das UI-Textfeld

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;  // Speichert die Position des letzten erreichten Checkpoints
    public Text warningText;  // UI-Textfeld für die Nachricht
    public float messageDuration = 3f;  // Dauer, wie lange die Nachricht angezeigt wird

    private void Start()
    {
        currentCheckpoint = transform.position;  // Setze dies auf die Position des Spielers
        warningText.gameObject.SetActive(false);  // Versteckt das Textfeld zu Beginn

        // Debugging, um sicherzustellen, dass warningText korrekt zugewiesen ist
        if (warningText == null)
        {
            Debug.LogError("Warning Text ist nicht zugewiesen! Bitte weise das Textfeld im Inspektor zu.");
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
        // Spieler wird zum letzten Checkpoint teleportiert
        transform.position = currentCheckpoint;
        // Startet eine Coroutine, um die Warnmeldung anzuzeigen
        StartCoroutine(ShowWarningMessage());
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