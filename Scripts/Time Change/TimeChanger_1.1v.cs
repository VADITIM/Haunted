using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    public Light directionalLight; // Reference to the directional light in the scene
    public float transitionDuration = 0f; // Duration of the transition from day to night

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeTimeOfDay();
        }
    }

    private void ChangeTimeOfDay()
    {
        Quaternion targetRotation = Quaternion.Euler(-77f, 111f, -129f);
        StartCoroutine(RotateLight(targetRotation));
    }

    private System.Collections.IEnumerator RotateLight(Quaternion targetRotation)
    {
        Quaternion initialRotation = directionalLight.transform.rotation;
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