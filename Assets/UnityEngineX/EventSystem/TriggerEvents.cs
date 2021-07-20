using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public bool debug;
    public List<string> TriggerTags;

    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.tag))
        {
            if (debug)
            {
                print("this.name " + name);
                print("other.name " + other.name);
                print("other.tag " + other.tag);
            }

            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TriggerTags.Contains(other.tag))
            OnTriggerExitEvent?.Invoke();
    }
}