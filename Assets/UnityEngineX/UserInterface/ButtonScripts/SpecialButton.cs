using System;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public enum SpecialButtonType
{
    NONE,
    PLAY,
    RATE_US,
    MORE_GAMES,
    PRIVACY_POLICY,
    SETTINGS,
    EXIT,
    CHANGE_SCENE,
}

[RequireComponent(typeof(Button))]
public class SpecialButton : MonoBehaviour
{
    public static readonly string Event_PlayClicked = GameDB.EventsIdentifier.PlayClicked.ToString();
    public static readonly string Event_RateUsClicked = GameDB.EventsIdentifier.RateUsClicked.ToString();
    public static readonly string Event_MoreGamesClicked = GameDB.EventsIdentifier.MoreGamesClicked.ToString();
    public static readonly string Event_PrivacyPolicyClicked = GameDB.EventsIdentifier.PrivacyPolicyClicked.ToString();
    public static readonly string Event_SettingsClicked = GameDB.EventsIdentifier.SettingsClicked.ToString();
    public static readonly string Event_ExitClicked = GameDB.EventsIdentifier.ExitClicked.ToString();
    public static readonly string Event_ChangeSceneClicked = GameDB.EventsIdentifier.ChangeSceneClicked.ToString();

    public SpecialButtonType buttonType = SpecialButtonType.NONE;
    
    // Privacy Policy Link
    [ConditionalField(nameof(buttonType),compareValues:SpecialButtonType.PRIVACY_POLICY)]
    public string privacyPolicyLink = "www.google.com";
    
    // Rate Us Link
    [ConditionalField(nameof(buttonType),compareValues:SpecialButtonType.RATE_US)]
    public string rateUsLink = "www.google.com";
    
    // More Games Link
    [ConditionalField(nameof(buttonType),compareValues:SpecialButtonType.MORE_GAMES)]
    public string moreGamesLink = "www.google.com";

    // Change Scene Index
	[ConditionalField( nameof( buttonType ), compareValues: SpecialButtonType.CHANGE_SCENE )]
	public int changeSceneIndex = 0;

	private void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick_ThisButton);
    }

    private void OnClick_ThisButton()
    {
        switch (buttonType)
        {
            case SpecialButtonType.NONE:
                Debug.LogWarning($"Special Button on Object ({gameObject.name}) is set to NONE; Please review its functionality");
                break;
            case SpecialButtonType.PLAY:
                gameObject.RaiseEvent(Event_PlayClicked);
                break;
            case SpecialButtonType.RATE_US:
                gameObject.RaiseEvent(Event_RateUsClicked,rateUsLink);
                break;
            case SpecialButtonType.MORE_GAMES:
                gameObject.RaiseEvent(Event_MoreGamesClicked,moreGamesLink);
                break;
            case SpecialButtonType.PRIVACY_POLICY:
                gameObject.RaiseEvent(Event_PrivacyPolicyClicked,privacyPolicyLink);
                break;
            case SpecialButtonType.SETTINGS:
                gameObject.RaiseEvent(Event_SettingsClicked);
                break;
            case SpecialButtonType.EXIT:
                gameObject.RaiseEvent(Event_ExitClicked);
                break;
            case SpecialButtonType.CHANGE_SCENE:
                gameObject.RaiseEvent( Event_ChangeSceneClicked , changeSceneIndex );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}