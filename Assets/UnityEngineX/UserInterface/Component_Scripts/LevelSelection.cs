using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	private void OnEnable()
	{
		gameObject.ConnectEvent( GameDB.EventsIdentifier.LevelClicked.ToString(), (sender, data)=> {
			LevelSelected( sender );
		} );
	}

	public void LevelSelected(GameObject levelButton)
	{
		// Handle Level Selection Here
		Debug.Log( levelButton.name );
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent( GameDB.EventsIdentifier.LevelClicked.ToString() );
	}
}