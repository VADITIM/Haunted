using UnityEngine;

public class RevealAbility : MonoBehaviour
{
    public GameObject revealPanel; // The UI panel that overlays the screen
    public LayerMask hiddenObjectsLayer; // The layer of hidden objects
    private bool isPanelVisible = false; // State of the ability
    private Camera mainCamera;
    private CanvasGroup panelCanvasGroup;

    void Start()
    {
        mainCamera = Camera.main;

        // Add a CanvasGroup to the panel if it doesn't already have one
        panelCanvasGroup = revealPanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = revealPanel.gameObject.AddComponent<CanvasGroup>();
        }

        isPanelVisible = false;

        HideRevealPanel();
        HideAllHiddenObjects();
    }

    void Update()
    {
        // If the panel is visible, render the hidden objects
        if (isPanelVisible)
        {
            RenderHiddenObjectsWithinPanel();
        }
    }

    public void ToggleReveal()
    {
        isPanelVisible = !isPanelVisible; // Toggle the visibility state
        if (isPanelVisible)
        {
            ShowRevealPanel();
        }
        else
        {
            HideRevealPanel();
            HideAllHiddenObjects(); // Hide the objects when the panel is closed
        }
    }

    private void ShowRevealPanel()
    {
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.blocksRaycasts = true; // Make the panel interactive when visible
    }

    private void HideRevealPanel()
    {
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.blocksRaycasts = false; // Make the panel non-interactive when invisible
    }

    private void RenderHiddenObjectsWithinPanel()
    {
        Vector3[] panelCorners = new Vector3[4];
        revealPanel.GetComponent<RectTransform>().GetWorldCorners(panelCorners);

        Vector3 bottomLeft = panelCorners[0];
        Vector3 topRight = panelCorners[2];

        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("hiddenObject");

        foreach (GameObject hiddenObject in hiddenObjects)
        {
            Renderer renderer = hiddenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 screenPos = mainCamera.WorldToScreenPoint(hiddenObject.transform.position);
                bool isInPanel = screenPos.x >= bottomLeft.x && screenPos.x <= topRight.x && screenPos.y >= bottomLeft.y && screenPos.y <= topRight.y;

                if (isInPanel)
                {
                    renderer.enabled = true;  // Show the object
                }
                else
                {
                    renderer.enabled = false; // Hide the object
                }
            }
        }
    }

    private void HideAllHiddenObjects()
    {
        GameObject[] hiddenObjects = GameObject.FindGameObjectsWithTag("hiddenObject");
        foreach (GameObject hiddenObject in hiddenObjects)
        {
            Renderer renderer = hiddenObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // Hide the object
            }
        }
    }
}