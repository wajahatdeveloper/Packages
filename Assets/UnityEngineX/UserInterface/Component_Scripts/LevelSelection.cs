using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelection : MonoBehaviour
{
	public UnityEvent OnLevelSelected;
	public UnityEvent<GameObject> OnLevelSelected_Obj;
	
	private void OnEnable()
	{
		gameObject.ConnectEvent( GameDB.EventsIdentifier.Clicked_Level.ToString(), (sender, data)=> {
			LevelSelected( sender );
		} );
	}

	public void LevelSelected(GameObject levelButton)
	{
		// Handle Level Selection Here
		Debug.Log( levelButton.name );
		OnLevelSelected?.Invoke();
		OnLevelSelected_Obj?.Invoke(levelButton);
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent( GameDB.EventsIdentifier.Clicked_Level.ToString() );
	}
}