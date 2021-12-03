using UnityEngine;
using System.Collections;

public class PlaySoundOnStart : MonoBehaviour
{
	public string audioID;

	void Start()
	{
		if (!string.IsNullOrEmpty( audioID ))
		{
			AudioController.Instance.PlaySound( audioID );
		}
	}
}
