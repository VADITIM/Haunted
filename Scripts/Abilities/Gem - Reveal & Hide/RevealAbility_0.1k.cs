using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAbility : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.T; // Taste zum An- und Ausschalten der Fähigkeit
    public GameObject revealPanel; // Das UI-Panel, das den Bildschirm überlagert
    public LayerMask hiddenObjectsLayer; // Der Layer der versteckten Objekte

    private bool isRevealing = false; // Zustand der Fähigkeit
    private Camera mainCamera;
    private Color originalCameraColor; // Um die Kamerafarbe zurückzusetzen

    void Start()
    {
        mainCamera = Camera.main;
        originalCameraColor = mainCamera.backgroundColor;

        // Mache das Panel zu Beginn der Szene unsichtbar
        if (revealPanel != null)
        {
            revealPanel.SetActive(false);
        }

        // Verstecke die Objekte zu Beginn
        SetObjectsVisibility(false);
    }

    void Update()
    {
        // Überprüfen, ob die Taste gedrückt wurde
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleReveal();
        }
    }

    void ToggleReveal()
    {
        isRevealing = !isRevealing;

        // Sicht umschalten
        if (isRevealing)
        {
            EnableReveal();
        }
        else
        {
            DisableReveal();
        }
    }

    void EnableReveal()
    {
        // Zeige das Panel an
        if (revealPanel != null)
        {
            revealPanel.SetActive(true);
        }

        // Ändere die Kamerafarbe oder -eigenschaften für den Effekt
        mainCamera.backgroundColor = Color.cyan; // Beispiel: die Kamerafarbe ändern
        mainCamera.clearFlags = CameraClearFlags.SolidColor;

        // Schalte versteckte Objekte sichtbar
        SetObjectsVisibility(true);
    }

    void DisableReveal()
    {
        // Setze die Kamera-Eigenschaften zurück
        mainCamera.backgroundColor = originalCameraColor;
        mainCamera.clearFlags = CameraClearFlags.Skybox;

        // Verstecke das Panel
        if (revealPanel != null)
        {
            revealPanel.SetActive(false);
        }

        // Verstecke die zuvor sichtbaren Objekte wieder
        SetObjectsVisibility(false);
    }

    void SetObjectsVisibility(bool isVisible)
    {
        // Alle Objekte im versteckten Layer sichtbar oder unsichtbar machen
        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("Hidden");

        foreach (GameObject obj in hiddenObjects)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            Collider objCollider = obj.GetComponent<Collider>();
            Rigidbody objRigidbody = obj.GetComponent<Rigidbody>();

            if (objRenderer != null)
            {
                objRenderer.enabled = isVisible;
            }

            if (objCollider != null)
            {
                objCollider.enabled = isVisible;
            }

            if (objRigidbody != null)
            {
                objRigidbody.isKinematic = !isVisible; // Aktiviert die Physik, wenn das Objekt sichtbar ist
            }
        }
    }
}