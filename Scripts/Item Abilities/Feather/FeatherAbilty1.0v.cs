using UnityEngine;

public class FeatherAbility : MonoBehaviour
{
    public GameObject areaPrefab; // Prefab for the area that destroys burnable objects
    public float destroyDelay = 10f; // The delay before the area is destroyed

    public void CreateDestroyArea()
    {
        // Get the cursor position in world space
        Vector3? cursorPosition = GetCursorWorldPosition();

        // Check if the cursor position is valid (raycast hit a collider)
        if (cursorPosition.HasValue)
        {
            // Instantiate the area at the cursor position
            GameObject area = Instantiate(areaPrefab, cursorPosition.Value, Quaternion.identity);

            // Destroy the area after the specified delay
            Destroy(area, destroyDelay);
        }
        else
        {
            Debug.Log("No collision detected within 5f");
        }
    }

    Vector3? GetCursorWorldPosition()
    {
        // Get the cursor position in screen space
        Vector3 cursorScreenPosition = Input.mousePosition;

        // Convert the screen position to a ray
        Ray ray = Camera.main.ScreenPointToRay(cursorScreenPosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 5f))
        {
            // Return the hit point if a collider is hit
            return hit.point;
        }

        // Return null if no collider is hit
        return null;
    }
}
