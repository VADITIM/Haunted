using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject DialogueTemplate;
    public GameObject Canvas;
    private Dialogue dialogueComponent;
    bool isInRange = false;

    void Start()
    {
        // Initialize the dialogue component
        dialogueComponent = Canvas.GetComponentInChildren<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            Canvas.SetActive(true);
            Debug.Log("Interacting with NPC");
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueComponent != null)
        {
            dialogueComponent.lines = new string[] {
                "Hello there!",
                "How are you?",
                "I am fine, thank you.",
            };
            dialogueComponent.StartDialogue();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}