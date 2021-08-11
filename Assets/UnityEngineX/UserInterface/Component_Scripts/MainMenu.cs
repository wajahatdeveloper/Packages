using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : RuleBehaviour
{
    [InfoBox("Subscribes to Events:\n" +
             "  Play Clicked\n" +
             "  Rate Us Clicked\n" +
             "  More Games Clicked\n" +
             "  Privacy Policy Clicked\n" +
             "  Settings Clicked\n" +
             "  Exit Clicked")]
    public Model_Variables dataModelVariables;

    private void OnEnable()
    {
        // ------------------- Rule 1 -------------------
        gameObject.ConnectEvent(SpecialButton.Event_PlayClicked,OnClick_Play);
        
        // ------------------- Rule 2 -------------------
        // ..
        
        gameObject.ConnectEvent(SpecialButton.Event_RateUsClicked,(sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_MoreGamesClicked,(sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_PrivacyPolicyClicked, (sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_SettingsClicked, ( sender, eventData ) => { } );
        gameObject.ConnectEvent(SpecialButton.Event_ExitClicked,(sender,eventData) => { });
    }

    private void OnClick_Play(GameObject sender, object data)
    {
        SceneManagerX.LoadNextScene();
    }

	private void OnDisable()
    {
        gameObject.DisconnectEvent(SpecialButton.Event_PlayClicked);
        gameObject.DisconnectEvent(SpecialButton.Event_RateUsClicked);
        gameObject.DisconnectEvent(SpecialButton.Event_MoreGamesClicked);
        gameObject.DisconnectEvent(SpecialButton.Event_PrivacyPolicyClicked);
        gameObject.DisconnectEvent(SpecialButton.Event_SettingsClicked);
        gameObject.DisconnectEvent(SpecialButton.Event_ExitClicked);
    }
}