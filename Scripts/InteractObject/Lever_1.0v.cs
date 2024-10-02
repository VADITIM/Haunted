using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public Vector3 doorMoveOffset;

    public Vector3 leverMoveOffset; // The offset for the lever's new position
    public Vector3 leverRotationOffset; // The offset for the lever's new rotation (Euler angles)

    public float moveSpeed = 2f;
    
    private bool isActivated = false;
    private Vector3 initialDoorPosition;
    private Vector3 targetDoorPosition;
    private Vector3 initialLeverPosition;
    private Quaternion initialLeverRotation;
    private Vector3 targetLeverPosition;
    private Quaternion targetLeverRotation;

    private AudioSource audioSource; 

    void Start()
    {
        initialDoorPosition = door.transform.position;
        targetDoorPosition = initialDoorPosition + doorMoveOffset;

        initialLeverPosition = transform.position;
        initialLeverRotation = transform.rotation;
        targetLeverPosition = initialLeverPosition + leverMoveOffset;
        targetLeverRotation = initialLeverRotation * Quaternion.Euler(leverRotationOffset);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isActivated)
        {
            MoveDoor();
            TransformLever();
        }
    }

    public void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            PlaySound(); 
        }
    }

    private void MoveDoor()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, targetDoorPosition, moveSpeed * Time.deltaTime);
    }

    private void TransformLever()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLeverPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLeverRotation, moveSpeed * Time.deltaTime * 100); 
    }

    private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}