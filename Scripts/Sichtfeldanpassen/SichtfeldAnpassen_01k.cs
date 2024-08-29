using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaleEffect : MonoBehaviour
{
    public Camera mainCamera; // Die Hauptkamera des Spielers
    public float targetFOV = 90f; // Ziel-Sichtfeld, um den Raum gr��er erscheinen zu lassen
    public float duration = 1.0f; // Dauer der Sichtfeld�nderung in Sekunden

    private float initialFOV; // Das urspr�ngliche Sichtfeld der Kamera
    private bool isEnlarging = false; // Gibt an, ob die Sichtfeld�nderung l�uft
    private float elapsedTime = 0f; // Zeit, die seit Beginn der Ver�nderung vergangen ist

    void Start()
    {
        // Speichere das urspr�ngliche Sichtfeld der Kamera
        initialFOV = mainCamera.fieldOfView;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnlarging)
        {
            // Starte die Sichtfeld�nderung, wenn der Spieler den Raum betritt
            StartCoroutine(EnlargeRoom());
        }
    }

    System.Collections.IEnumerator EnlargeRoom()
    {
        isEnlarging = true;
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Berechne das neue Sichtfeld basierend auf der verstrichenen Zeit
            mainCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Setze das Sichtfeld auf das Zielwert
        mainCamera.fieldOfView = targetFOV;
        isEnlarging = false;
    }
}
