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
                return;
            }
            if (textComponent.text == lines[index]) 
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines(); 
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
            StartCoroutine(TypeLine()); 
        }
        else
        {
            PlayerMotor.dialogue = false;
            gameObject.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        if (lines.Length < 1)
        {
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