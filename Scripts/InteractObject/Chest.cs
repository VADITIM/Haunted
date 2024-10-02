using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestLid;
    public Vector3 openRotation; // Set the open rotation (e.g., new Vector3(-90, 0, 0))
    public Vector3 closedRotation; // Set the closed rotation (e.g., new Vector3(0, 0, 0))
    public float rotationSpeed = 2.0f; // Speed of rotation

    public AudioClip toggleSound; // Sound to play when the chest is toggled

    private AudioSource audioSource; // AudioSource component
    private bool open = false;
    private bool isAnimating = false;

    void Start()
    {
        chestLid.transform.rotation = Quaternion.Euler(closedRotation);
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public IEnumerator ToggleChest()
    {
        if (isAnimating)
            yield break;

        isAnimating = true;

        Vector3 targetRotation = open ? closedRotation : openRotation;
        Quaternion startRot = chestLid.transform.rotation;
        Quaternion endRot = Quaternion.Euler(targetRotation);

        audioSource.PlayOneShot(toggleSound);

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            chestLid.transform.rotation = Quaternion.Lerp(startRot, endRot, time);
            yield return null;
        }
        chestLid.transform.rotation = endRot;

        open = !open;
        isAnimating = false;
    }
}