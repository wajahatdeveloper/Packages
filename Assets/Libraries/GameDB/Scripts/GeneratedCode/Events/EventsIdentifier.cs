namespace GameDB
{
	//Generated code, do not edit!

	public enum EventsIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("Clicked_Settings")] Clicked_Settings = 1,
		[Identifier("Clicked_Play")] Clicked_Play = 2,
		[Identifier("Clicked_MoreGames")] Clicked_MoreGames = 3,
		[Identifier("Clicked_RateUs")] Clicked_RateUs = 4,
		[Identifier("Clicked_PrivacyPolicy")] Clicked_PrivacyPolicy = 5,
		[Identifier("Clicked_Level")] Clicked_Level = 7,
		[Identifier("Game_Exit_Begin")] Game_Exit_Begin = 6,
		[Identifier("Scene_Changed")] Scene_Changed = 8,
		[Identifier("Game_Paused")] Game_Paused = 9,
		[Identifier("Game_Resumed")] Game_Resumed = 10,
		[Identifier("Toggle_Sound")] Toggle_Sound = 11,
		[Identifier("Toggle_Music")] Toggle_Music = 12,
		[Identifier("Toggle_Audio")] Toggle_Audio = 13,
		[Identifier("Game_Restart_Begin")] Game_Restart_Begin = 14,
		[Identifier("Game_ToBack")] Game_ToBack = 15,
	}

	public static class EventsIdentifierExtension
	{
		public static EventsRecord GetRecord(this EventsIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.EventsModel.GetRecord(identifier, editableRecord);
		}
	}
}
