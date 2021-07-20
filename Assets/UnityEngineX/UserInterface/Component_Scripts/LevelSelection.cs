using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	private void Start()
	{
		gameObject.ConnectEvent( SheetCodes.EventsIdentifier.Levelclicked.ToString(), (sender, data)=> {
			LevelSelected( sender );
		} );
	}

	public void LevelSelected(GameObject levelButton)
	{
		// Handle Level Selection Here
		Debug.Log( levelButton.name );
	}
}