using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{
    public GameObject roomParent; // Das �bergeordnete Objekt des Raums, das skaliert werden soll
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); // Die gew�nschte Zielskalierung des Raums
    public float targetFOV = 70f; // Das Ziel-Sichtfeld f�r die Kamera
    public float fovChangeDuration = 1.0f; // Dauer f�r die FOV-�nderung

    private float originalFOV;
    private Camera mainCamera;

    private void Awake()
    {
        // Skaliere den Raum sofort auf die gew�nschte Gr��e
        roomParent.transform.localScale = targetScale;

        // Hole die Hauptkamera
        mainCamera = Camera.main;

        // Speichere das urspr�ngliche Sichtfeld
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pr�fe, ob der Spieler den Trigger betreten hat
        if (other.CompareTag("Player"))
        {
            // F�hre die FOV-�nderung durch
            if (mainCamera != null)
            {
                StopAllCoroutines(); // Stoppe laufende Koroutinen
                StartCoroutine(ChangeFOV(targetFOV));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Pr�fe, ob der Spieler den Trigger verl�sst
        if (other.CompareTag("Player"))
        {
            // Setze das FOV zur�ck auf den Originalwert
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
            // Interpoliere das FOV �ber die Zeit
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, newFOV, elapsed / fovChangeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Setze das FOV auf den Zielwert
        mainCamera.fieldOfView = newFOV;
    }
}