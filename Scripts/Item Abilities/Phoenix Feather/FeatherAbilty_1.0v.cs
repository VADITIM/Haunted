using UnityEngine;

public class FeatherAbility : MonoBehaviour
{
    public GameObject areaPrefab;
    public float destroyDelay = 10f;

    public void CreateDestroyArea()
    {
        Vector3? cursorPosition = GetCursorWorldPosition();

        if (cursorPosition.HasValue)
        {
            GameObject area = Instantiate(areaPrefab, cursorPosition.Value, Quaternion.identity);
            Destroy(area, destroyDelay);
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
}