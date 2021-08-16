using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineX;

public class ActivationSequence : MonoBehaviour
{
    public bool customSequence;
    
    public NavigationList<GameObject> sequence;

    public UnityEvent OnSequnceEnd;

    private void OnEnable()
    {
        if (!customSequence)
        {
            foreach (Transform child in transform)
            {
                sequence.Add(child.gameObject);
            }
        }

        sequence.OnSequenceEndReached += () => OnSequnceEnd?.Invoke();
        
        sequence.Current.SetActive(false);
        sequence.CurrentIndex = 0;
        sequence.Current.SetActive(true);
    }

    public void Next()
    {
        sequence.Current.SetActive(false);
        sequence.MoveNext.SetActive(true);
    }

    public void Previous()
    {
        sequence.Current.SetActive(false);
        sequence.MovePrevious.SetActive(true);
    }
}