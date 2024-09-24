using UnityEngine;

public class FlowerAbility : MonoBehaviour
{
    public GameObject objectPrefab; 
    public float targetScale = 6f; // The target scale size
    public float scaleDuration = 1f; // The duration over which the object scales
    public float destroyDelay = 10f; // The delay before the object is destroyed

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            SpawnAndScaleObject();
            Debug.Log("Spawned and scaled object");
        }
    }

    public void SpawnAndScaleObject()
    {
        // Get the cursor position in world space
        Vector3? cursorPosition = GetCursorWorldPosition();

        // Check if the cursor position is valid (raycast hit a collider)
        if (cursorPosition.HasValue)
        {
            // Calculate the bottom position of the object
            Vector3 spawnPosition = cursorPosition.Value;
            float objectHeight = objectPrefab.GetComponent<Renderer>().bounds.size.y;
            spawnPosition.y += objectHeight / 2;

            // Instantiate the object at the adjusted position
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Start the scaling coroutine
            StartCoroutine(ScaleObject(spawnedObject, targetScale, scaleDuration));

            // Destroy the object after the specified delay
            Destroy(spawnedObject, destroyDelay);
        }
        else
        {
            Debug.Log("No collider hit. Object not spawned.");
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

    System.Collections.IEnumerator ScaleObject(GameObject obj, float targetScale, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScaleVector, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScaleVector;
    }
}