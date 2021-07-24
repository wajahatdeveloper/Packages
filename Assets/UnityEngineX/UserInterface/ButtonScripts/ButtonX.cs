using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngineX
{
	[RequireComponent( typeof( Button ) )]
	public class ButtonX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		[FoldoutGroup( "Click Action" )]
		public UnityEvent<ButtonX> OnClick;

		[FoldoutGroup( "Click Sound" )] public bool clickSound;
		[FoldoutGroup( "Click Sound" )] public string customSoundID = "custom";
		[FoldoutGroup( "Click Sound" )] public bool useCustomSound;
		[FoldoutGroup( "Click Sound" ), ShowIf( nameof( useCustomSound ) )] public AudioClip customSound;

		[FoldoutGroup( "Click Events" )] public UnityEvent<ButtonX> OnPointerDown;
		[FoldoutGroup( "Click Events" )] public UnityEvent<ButtonX> OnPointerUp;
		[FoldoutGroup( "Click Events" )] public UnityEvent<ButtonX> OnPointerEnter;
		[FoldoutGroup( "Click Events" )] public UnityEvent<ButtonX> OnPointerExit;

		public object Data
		{
			get => _data;
			set => _data = value;
		}

		public Button Button
		{
			get => _button;
			set => _button = value;
		}

		private object _data;
		private Button _button;

		private void Start()
		{
			if (clickSound) { AudioController.instance.AddSound( "Click" + customSoundID, customSound ); }
			_button = GetComponent<Button>();
			_button.onClick.AddListener( () =>
			{
				OnClick?.Invoke( this );
				if (clickSound) { AudioController.instance.PlaySound( useCustomSound ? "Click" + customSoundID : "Click" ); }
			} );
		}

		void IPointerEnterHandler.OnPointerEnter( PointerEventData eventData )
		{
			OnPointerEnter?.Invoke( this );
		}

		void IPointerExitHandler.OnPointerExit( PointerEventData eventData )
		{
			OnPointerExit?.Invoke( this );
		}

		void IPointerDownHandler.OnPointerDown( PointerEventData eventData )
		{
			OnPointerDown?.Invoke( this );
		}

		void IPointerUpHandler.OnPointerUp( PointerEventData eventData )
		{
			OnPointerUp?.Invoke( this );
		}
	}
}