using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events 
{
    public enum EventsIdentifier
    {
        None = 0,
        Clicked_Settings = 1,
        Clicked_Play = 2,
        Clicked_MoreGames = 3,
        Clicked_RateUs = 4,
        Clicked_PrivacyPolicy = 5,
        Clicked_Level = 7,
        Game_Exit_Begin = 6,
        Scene_Changed = 8,
        Game_Paused = 9,
        Game_Resumed = 10,
        Toggle_Sound = 11,
        Toggle_Music = 12,
        Toggle_Audio = 13,
        Game_Restart_Begin = 14,
        Game_ToBack = 15,
    }
}