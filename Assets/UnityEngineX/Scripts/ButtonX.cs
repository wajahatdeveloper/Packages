using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngineX
{
	public class ButtonX : MonoBehaviour
	{
		[Foldout("Click Sound",true)]
		public bool clickSound;
		public int soundID;
		public bool useCustomSound;
		[ConditionalField( "useCustomSound" )]
			public AudioClip customSound;

		private Button button;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener( () => {
				if (clickSound)
				{
					if (useCustomSound)
					{
						AudioManager.PlayUISound( customSound );
					}
					else
					{
						AudioManager.GetUISoundAudio( soundID ).Play();
					}
				}
			} );
		}
	}
}