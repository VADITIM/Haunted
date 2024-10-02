using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color currentColor;
    private float transitionSpeed = 1.0f;
    private float transitionProgress = 0.0f;
    private int colorIndex = 0;

    private Color[] colors = new Color[]
    {
        new Color(1f, 0f, 0f, 0.5f), 
        new Color(0f, 1f, 0f, 0.5f),
        new Color(0f, 0f, 1f, 0.5f) 
    };

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        currentColor = colors[colorIndex];
        objectRenderer.material.color = currentColor;

        // Ensure the material's shader supports transparency
        objectRenderer.material.SetFloat("_Mode", 3);
        objectRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objectRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objectRenderer.material.SetInt("_ZWrite", 0);
        objectRenderer.material.DisableKeyword("_ALPHATEST_ON");
        objectRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        objectRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objectRenderer.material.renderQueue = 3000;
    }

    void Update()
    {
        transitionProgress += Time.deltaTime * transitionSpeed;

        if (transitionProgress >= 1.0f)
        {
            transitionProgress = 0.0f;
            colorIndex = (colorIndex + 1) % colors.Length;
        }

        Color nextColor = colors[(colorIndex + 1) % colors.Length];
        currentColor = Color.Lerp(colors[colorIndex], nextColor, transitionProgress);
        objectRenderer.material.color = currentColor;
    }
}