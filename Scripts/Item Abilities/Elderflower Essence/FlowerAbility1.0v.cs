using UnityEngine;

public class FlowerAbility : MonoBehaviour
{
    public GameObject objectPrefab; 
    public float targetScale = 6f;
    public float scaleDuration = 1f; 
    public float destroyDelay = 10f; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            SpawnAndScaleObject();
        }
    }

    public void SpawnAndScaleObject()
    {
        Vector3? cursorPosition = GetCursorWorldPosition();

        if (cursorPosition.HasValue)
        {
            Vector3 spawnPosition = cursorPosition.Value;
            float objectHeight = objectPrefab.GetComponent<Renderer>().bounds.size.y;
            spawnPosition.y += objectHeight / 2;

            Quaternion initialRotation = Quaternion.Euler(90, 0, 0);

            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, initialRotation);

            StartCoroutine(ScaleObject(spawnedObject, targetScale, scaleDuration));

            Destroy(spawnedObject, destroyDelay);
        }
    }

    Vector3? GetCursorWorldPosition()
    {
        Vector3 cursorScreenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(cursorScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            return hit.point;
        }

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