using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineX;

public class RaiseEvent : MonoBehaviour
{
	private static IEnumerable<string> EventsIdentifiers = Enum.GetNames( typeof( Events.EventsIdentifier ) );

	public bool autoHookButton = true;
	[ValueDropdown( "EventsIdentifiers", IsUniqueList = true, DisableListAddButtonBehaviour = true, NumberOfItemsBeforeEnablingSearch = 1 )]
	public string eventIdentifier;

    private void Start()
    {
        if (autoHookButton)
        {
            GetComponent<Button>()?.onClick.AddListener( RaiseDefaultEvent );
        }
    }

    public void RaiseDefaultEvent()
    {
        gameObject.RaiseEvent(eventIdentifier);
    }
    
    public void RaiseCustomEvent(string evtId)
    {
        gameObject.RaiseEvent(evtId);
    }
}