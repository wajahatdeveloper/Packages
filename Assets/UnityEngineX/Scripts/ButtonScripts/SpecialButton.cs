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
}

[RequireComponent(typeof(Button))]
public class SpecialButton : MonoBehaviour
{
    public static readonly string Event_PlayClicked = "Event_PlayClicked";
    public static readonly string Event_RateUsClicked = "Event_RateUsClicked";
    public static readonly string Event_MoreGamesClicked = "Event_MoreGamesClicked";
    public static readonly string Event_PrivacyPolicyClicked = "Event_PrivacyPolicyClicked";
    public static readonly string Event_SettingsClicked = "Event_SettingsClicked";
    public static readonly string Event_ExitClicked = "Event_ExitClicked";
    
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}