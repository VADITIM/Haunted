using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; // Reference to the Dialogue script
    int index = -100; // Start index at 0

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            if (PlayerMotor.dialogue)
            {
                if (index > 0) 
                {
                    transform.GetChild(index).gameObject.SetActive(false); // Deactivate the previous dialogue element
                }
                index++;
                if (index < transform.childCount) 
                {
                    dialogueScript.NextLine(); // Only move to next line if there's more content
                }
                else
                {
                    index = 0; // Reset index when out of children
                    PlayerMotor.dialogue = false;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}