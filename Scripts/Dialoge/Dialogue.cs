using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lines.Length < 1)
            {
                Debug.LogError("No lines available in the dialogue.");
                return;
            }
            if (textComponent.text == lines[index]) // Only move to next line if current one is fully shown
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines(); // Show the full line if it's incomplete
                textComponent.text = lines[index];
            }
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine()); // Begin typing the next line
        }
        else
        {
            PlayerMotor.dialogue = false;
            gameObject.SetActive(false); // Close dialogue box
        }
    }

    public void StartDialogue()
    {
        if (lines.Length < 1)
        {
            Debug.LogError("No lines available in the dialogue.");
            return;
        }
        index = 0;
        PlayerMotor.dialogue = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}