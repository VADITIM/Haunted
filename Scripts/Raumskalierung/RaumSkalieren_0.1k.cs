using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{
    public Transform roomParent; // Der übergeordnete Raum (Elternobjekt, das alle Raumteile enthält)
    public float targetScale = 2.0f; // Zielskalierung, um den Raum größer erscheinen zu lassen
    public float scaleDuration = 1.0f; // Dauer der Skalierungsänderung

    private Vector3 initialScale; // Die ursprüngliche Skalierung des Raums
    private bool isScaling = false; // Gibt an, ob die Skalierung läuft
    private float elapsedTime = 0f; // Zeit, die seit Beginn der Skalierung vergangen ist

    void Start()
    {
        // Speichere die ursprüngliche Skalierung des Raums
        initialScale = roomParent.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isScaling)
        {
            // Starte die Skalierungsänderung, wenn der Spieler den Raum betritt
            StartCoroutine(ScaleRoom());
        }
    }

    System.Collections.IEnumerator ScaleRoom()
    {
        isScaling = true;
        elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            // Berechne die neue Skalierung basierend auf der verstrichenen Zeit
            roomParent.localScale = Vector3.Lerp(initialScale, initialScale * targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Setze die Skalierung auf den Zielwert
        roomParent.localScale = initialScale * targetScale;
        isScaling = false;
    }

    void ResetColliders(GameObject room)
    {
        Collider[] colliders = room.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;   // Deaktiviere Collider
            col.enabled = true;    // Reaktiviere Collider
        }
    }
}