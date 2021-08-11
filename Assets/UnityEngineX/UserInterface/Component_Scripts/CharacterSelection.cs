using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterSelection : RuleBehaviour
{
	[InfoBox("Subscribes to Events:\n" +
	         "  Play Clicked")]
	public Model_Variables dataModelVariables;
	
	private void OnEnable()
	{
		// ------------------- Rule 1 -------------------
		gameObject.ConnectEvent(SpecialButton.Event_PlayClicked,OnClick_Play);
	}

	private void OnClick_Play(GameObject sender, object data)
	{
		SceneManagerX.LoadNextScene();
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent(SpecialButton.Event_PlayClicked);
	}
	
	public void OnCharacterSelected(GameObject selectedObject)
	{
		// ------------------- Rule 2 -------------------
		// Save Name of Selected Character to Persistent Data store
		PlayerPrefs.SetString(Constants.SELECTED_CHARACTER_ID,selectedObject.name);
		Debug.Log("Character Selection: Selected Character " + selectedObject.name);
	}
	
	// ------------------- Rule 3 -------------------
	// ..
}