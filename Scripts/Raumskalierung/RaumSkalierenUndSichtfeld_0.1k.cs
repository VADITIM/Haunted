using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{
    public GameObject roomParent; // Das übergeordnete Objekt des Raums, das skaliert werden soll
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); // Die gewünschte Zielskalierung des Raums
    public float targetFOV = 70f; // Das Ziel-Sichtfeld für die Kamera
    public float fovChangeDuration = 1.0f; // Dauer für die FOV-Änderung

    private float originalFOV;
    private Camera mainCamera;

    private void Awake()
    {
        // Skaliere den Raum sofort auf die gewünschte Größe
        roomParent.transform.localScale = targetScale;

        // Hole die Hauptkamera
        mainCamera = Camera.main;

        // Speichere das ursprüngliche Sichtfeld
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prüfe, ob der Spieler den Trigger betreten hat
        if (other.CompareTag("Player"))
        {
            // Führe die FOV-Änderung durch
            if (mainCamera != null)
            {
                StopAllCoroutines(); // Stoppe laufende Koroutinen
                StartCoroutine(ChangeFOV(targetFOV));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Prüfe, ob der Spieler den Trigger verlässt
        if (other.CompareTag("Player"))
        {
            // Setze das FOV zurück auf den Originalwert
            if (mainCamera != null)
            {
                StopAllCoroutines(); // Stoppe laufende Koroutinen
                StartCoroutine(ChangeFOV(originalFOV));
            }
        }
    }

    private System.Collections.IEnumerator ChangeFOV(float newFOV)
    {
        float elapsed = 0.0f;
        float startFOV = mainCamera.fieldOfView;

        while (elapsed < fovChangeDuration)
        {
            // Interpoliere das FOV über die Zeit
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, newFOV, elapsed / fovChangeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Setze das FOV auf den Zielwert
        mainCamera.fieldOfView = newFOV;
    }
}