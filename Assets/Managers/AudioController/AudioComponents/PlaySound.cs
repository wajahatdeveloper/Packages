using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
	public string soundId;

	public void PlaySFX( string id )
	{
		AudioController.instance.PlaySound( id );
	}

	public void PlaySFX_Default()
	{
		AudioController.instance.PlaySound( soundId );
	}
}