using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;
using System;

public class OnEvent : MonoBehaviour
{
	private static IEnumerable<string> EventsIdentifiers = Enum.GetNames( typeof( SheetCodes.EventsIdentifier ) );

	[ValueDropdown( "EventsIdentifiers" , IsUniqueList = true , DisableListAddButtonBehaviour = true , NumberOfItemsBeforeEnablingSearch = 1 )]
	public string eventIdentifier;
	public UnityEvent OnEventReceived;
	public UnityEvent<string> OnEventReceived_String;

	private void OnEnable()
	{
		if (eventIdentifier.IsNullOrEmpty())
		{
			Debug.LogError( $"{name} is missing Event Receive Identifier" );
		}
		gameObject.ConnectEvent( "Event_" + eventIdentifier, EventCall );
	}

	private void EventCall( GameObject sender, object eventData )
	{
		if (eventData.IsNull()) { OnEventReceived?.Invoke(); }
		string str = eventData as string;
		OnEventReceived_String?.Invoke( str );
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent( "Event_" + eventIdentifier );
	}
}