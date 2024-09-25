using UnityEngine;

public class HornAbility : MonoBehaviour
{
    private bool isLightOn = false; 

    public void ToggleLight(Light externalLight)
    {
        if (externalLight != null)
        {
            if (isLightOn)
            {
                externalLight.enabled = false;
            }
            else
            {
                externalLight.enabled = true;
            }

            isLightOn = !isLightOn;
        }
        else
        {
            Debug.LogError("External light is not assigned.");
        }
    }
}
