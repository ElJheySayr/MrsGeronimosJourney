using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public virtual void Interaction()
    {
        onInteraction.Invoke();
    }

    public virtual void DisableOutline()
    {
        outline.enabled = false;
    }

    public virtual void EnableOutline()
    {
        outline.enabled = true;
    }
}
