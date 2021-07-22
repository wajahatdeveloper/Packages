using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventExtensions;

namespace GameDB
{
	public static class EventsIdentifierExt
	{
		public static void ConnectEvent( this EventsIdentifier identifier, GameObject listener, EventWithData func )
		{
			listener.ConnectEvent( identifier.ToString(), func );
		}

		public static void DisconnectEvent( this EventsIdentifier identifier, GameObject listener )
		{
			listener.DisconnectEvent( identifier.ToString() );
		}
	}
}