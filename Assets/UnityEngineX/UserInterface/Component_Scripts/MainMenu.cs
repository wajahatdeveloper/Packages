using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : SingletonBehaviour<MainMenu>
{
    public int nextSceneIndex = 4;

    private void OnEnable()
    {
        gameObject.ConnectEvent(SpecialButton.Event_PlayClicked,OnClick_Play);
        gameObject.ConnectEvent(SpecialButton.Event_RateUsClicked,(sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_MoreGamesClicked,(sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_PrivacyPolicyClicked, (sender,eventData) => { });
        gameObject.ConnectEvent(SpecialButton.Event_SettingsClicked, ( sender, eventData ) => { } );
        gameObject.ConnectEvent(SpecialButton.Event_ExitClicked,(sender,eventData) => { });
    }

    private void OnClick_Play(GameObject sender, object data)
    {
        SceneManager.LoadScene(nextSceneIndex);
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