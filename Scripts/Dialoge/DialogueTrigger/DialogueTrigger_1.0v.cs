using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TMP_Text dialogueText;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public GameObject objectToActivate;
    public GameObject invisibleWalls;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool hasTriggered = false; 
    private float nextSkipTime = .5f; 


    void Start()
    {
        dialogueCanvas.SetActive(false);
        {
            objectToActivate.SetActive(false); 
            invisibleWalls.SetActive(false); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            playerInRange = true;
            dialogueCanvas.SetActive(true);
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); 
                invisibleWalls.SetActive(true); 
            }
            ShowNextLine(); 
            hasTriggered = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void DisplayLine(string line)
    {
        dialogueText.text = line; 
    }

    void Update()
    {
        if (playerInRange && Input.GetMouseButtonDown(0) && Time.time >= nextSkipTime)
        {
            ShowNextLine();
            nextSkipTime = Time.time + 1f; 
        }
    }

    void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            DisplayLine(dialogueLines[currentLine]);
            currentLine++;
        }
        else
        {
            HideDialogueCanvas();
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false); 
                invisibleWalls.SetActive(false); 
            }
        }
    }

    void HideDialogueCanvas()
    {
        dialogueCanvas.SetActive(false);
    }

    public Vector3 GetActivatedObjectPosition()
    {
        if (objectToActivate != null)
        {
            return objectToActivate.transform.position;
        }
        return Vector3.zero;
    }
}