using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{
    public Transform roomParent; // Der �bergeordnete Raum (Elternobjekt, das alle Raumteile enth�lt)
    public float targetScale = 2.0f; // Zielskalierung, um den Raum gr��er erscheinen zu lassen
    public float scaleDuration = 1.0f; // Dauer der Skalierungs�nderung

    private Vector3 initialScale; // Die urspr�ngliche Skalierung des Raums
    private bool isScaling = false; // Gibt an, ob die Skalierung l�uft
    private float elapsedTime = 0f; // Zeit, die seit Beginn der Skalierung vergangen ist

    void Start()
    {
        // Speichere die urspr�ngliche Skalierung des Raums
        initialScale = roomParent.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isScaling)
        {
            // Starte die Skalierungs�nderung, wenn der Spieler den Raum betritt
            StartCoroutine(ChangeScale(initialScale * targetScale));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isScaling)
        {
            // Setze die Skalierung zur�ck, wenn der Spieler den Raum verl�sst
            StartCoroutine(ChangeScale(initialScale));
        }
    }

    System.Collections.IEnumerator ChangeScale(Vector3 targetScale)
    {
        isScaling = true;
        elapsedTime = 0f;

        Vector3 startScale = roomParent.localScale;

        while (elapsedTime < scaleDuration)
        {
            // Berechne die neue Skalierung basierend auf der verstrichenen Zeit
            roomParent.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        roomParent.localScale = targetScale;
        isScaling = false;
    }
}