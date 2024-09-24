using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    public Light directionalLight; // Reference to the directional light in the scene
    public float transitionDuration = 2f; // Duration of the transition from day to night

    private bool isDay = true; // Flag to track whether it is currently day

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeTimeOfDay();
        }
    }

    private void ChangeTimeOfDay()
    {
        float targetRotationY = directionalLight.transform.eulerAngles.y + 90f;
        StartCoroutine(RotateLight(targetRotationY));
        isDay = !isDay;
    }

    private System.Collections.IEnumerator RotateLight(float targetRotationY)
    {
        Quaternion initialRotation = directionalLight.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(directionalLight.transform.eulerAngles.x, targetRotationY, directionalLight.transform.eulerAngles.z);
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            directionalLight.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        directionalLight.transform.rotation = targetRotation;
    }
}