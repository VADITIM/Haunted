using UnityEngine;
using UnityEngine.UI; // Für das Arbeiten mit UI-Elementen

public class RevealPanel : MonoBehaviour
{
    public Material hiddenObjectMaterial; // Material für versteckte Objekte
    public RectTransform revealPanel;     // Das Panel, innerhalb dessen die Objekte sichtbar werden
    public Image maskImage;               // Das Masken-Image, das zusammen mit dem Panel erscheint
    public Camera mainCamera;             // Die Hauptkamera
    public LayerMask hiddenObjectLayer;   // Die Layer der versteckten Objekte

    private CanvasGroup panelCanvasGroup; // CanvasGroup zum Steuern der Sichtbarkeit des Panels
    private bool isPanelVisible = false;   // Status, ob das Panel sichtbar ist

    private void Start()
    {
        // Füge eine CanvasGroup zum Panel hinzu, wenn es noch keine gibt
        panelCanvasGroup = revealPanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = revealPanel.gameObject.AddComponent<CanvasGroup>();
        }

        // Setze das Panel und die versteckten Objekte zu Beginn auf unsichtbar
        HideRevealPanel();
        HideMaskImage();
        HideAllHiddenObjects();
    }

    private void Update()
    {
        // Überprüfe, ob die Sicht aktiviert werden soll
        if (Input.GetKeyDown(KeyCode.T))
        {
            isPanelVisible = !isPanelVisible; // Umschalten des Sichtstatus
            if (isPanelVisible)
            {
                ShowRevealPanel();
                ShowMaskImage();
            }
            else
            {
                HideRevealPanel();
                HideMaskImage();
                HideAllHiddenObjects(); // Verstecke die Objekte, wenn das Panel geschlossen wird
            }
        }

        // Wenn das Panel sichtbar ist, render die versteckten Objekte
        if (isPanelVisible)
        {
            RenderHiddenObjectsWithinPanel();
        }
    }

    private void ShowRevealPanel()
    {
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.blocksRaycasts = true; // Panel interaktiv machen, wenn sichtbar
    }

    private void HideRevealPanel()
    {
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.blocksRaycasts = false; // Panel nicht interaktiv, wenn unsichtbar
    }

    private void ShowMaskImage()
    {
        maskImage.enabled = true; // Zeige das Masken-Image an
    }

    private void HideMaskImage()
    {
        maskImage.enabled = false; // Verstecke das Masken-Image
    }

    private void RenderHiddenObjectsWithinPanel()
    {
        Vector3[] panelCorners = new Vector3[4];
        revealPanel.GetWorldCorners(panelCorners);

        Vector3 bottomLeft = panelCorners[0];
        Vector3 topRight = panelCorners[2];

        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("HiddenObject");

        foreach (GameObject hiddenObject in hiddenObjects)
        {
            Renderer renderer = hiddenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 screenPos = mainCamera.WorldToScreenPoint(hiddenObject.transform.position);
                bool isInPanel = screenPos.x >= bottomLeft.x && screenPos.x <= topRight.x && screenPos.y >= bottomLeft.y && screenPos.y <= topRight.y;

                Debug.Log($"Panel Corners: {bottomLeft}, {topRight}, Hidden Object Position: {hiddenObject.transform.position}, Screen Position: {screenPos}, Is In Panel: {isInPanel}");

                if (isInPanel)
                {
                    renderer.enabled = true;  // Zeige das Objekt an
                    renderer.material = hiddenObjectMaterial;  // Wende das Material an
                }
                else
                {
                    renderer.enabled = false; // Verstecke das Objekt
                }
            }
        }
    }

    private void HideAllHiddenObjects()
    {
        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("HiddenObject");
        foreach (GameObject hiddenObject in hiddenObjects)
        {
            Renderer renderer = hiddenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // Verstecke das Objekt
            }
        }
    }
}