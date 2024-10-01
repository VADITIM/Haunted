using UnityEngine;

public class PlayerInteractObjects : MonoBehaviour
{
    public Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.CompareTag("lever"))
            {
                Lever lever = hitInfo.collider.GetComponent<Lever>();
                if (lever != null)
                {
                    lever.Interact();
                }
            }
        }
    }
}