using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
	public string soundId;

	public void PlaySFX( string id )
	{
		AudioController.Instance.PlaySound( id );
	}

	public void PlaySFX_Default()
	{
		AudioController.Instance.PlaySound( soundId );
	}
}