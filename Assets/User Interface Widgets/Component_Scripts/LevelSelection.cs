using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelection : RuleBehaviour
{
	[InfoBox("Subscribes to Events:\n" +
	         "  Level Clicked")]
	[PropertySpace(SpaceAfter = 20)]
	public Model_Variables dataModelVariables;
	
	public UnityEvent OnLevelSelected;
	public UnityEvent<GameObject> OnLevelSelected_Obj;
	
	private void OnEnable()
	{
		// ------------------- Rule 1 -------------------
		gameObject.ConnectEvent( GameDB.EventsIdentifier.Clicked_Level.ToString(), LevelSelected );
	}

	private void LevelSelected(GameObject levelButton, object data)
	{
		// Save Name of Selected Level to Persistent Data store
		PlayerPrefs.SetString(Constants.SELECTED_LEVEL_ID,levelButton.name);
		Debug.Log("Level Selection: Selected Level is " + levelButton.name);
		// Call Event Related to Level Selection
		OnLevelSelected?.Invoke();
		OnLevelSelected_Obj?.Invoke(levelButton);
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent( GameDB.EventsIdentifier.Clicked_Level.ToString() );
	}
	
	// ------------------- Rule 2 -------------------
	// ..
}