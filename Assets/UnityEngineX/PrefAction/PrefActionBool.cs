using ByteSheep.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefActionBool : MonoBehaviour
{
	public PrefActionManager.Prefs Pref;
	public int id = 0;
	public QuickEvent OnPrefTrue;
	public QuickEvent OnPrefFalse;

	private void OnEnable()
	{
		string prefString = Pref.ToString() + id.ToString();
		if (PlayerPrefs.GetInt(prefString, 0) == 1)
		{
			OnPrefTrue?.Invoke();
		}
		else
		{
			OnPrefFalse?.Invoke();
		}
	}
}