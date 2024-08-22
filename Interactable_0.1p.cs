using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisabledOutline();
    }

    public void Interact()
    { 
        onInteraction.Invoke ();
    }


    public void DisabledOutline()
    {
        outline.enabled = false;
    
    }

    public void EnableOutline()
    { 
        outline.enabled = true;
    
    }
} 