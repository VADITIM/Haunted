using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; 
    int index = -100; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            if (PlayerMotor.dialogue)
            {
                if (index > 0) 
                {
                    transform.GetChild(index).gameObject.SetActive(false); 
                }
                index++;
                if (index < transform.childCount) 
                {
                    dialogueScript.NextLine(); // Only move to next line if there's more content
                }
                else
                {
                    index = 0; // Reset index when done
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