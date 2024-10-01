using UnityEngine;

public class Saw : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    public Vector3 moveDirection = new Vector3(0, 0, 1); 
    public float moveDistance = 2f; 
    public float moveSpeed = 2f; 

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = initialPosition + moveDirection * offset;
    }
}