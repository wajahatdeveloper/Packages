namespace GameDB
{
	//Generated code, do not edit!

	public enum EventsIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("SettingsClicked")] SettingsClicked = 1,
		[Identifier("PlayClicked")] PlayClicked = 2,
		[Identifier("MoreGamesClicked")] MoreGamesClicked = 3,
		[Identifier("RateUsClicked")] RateUsClicked = 4,
		[Identifier("PrivacyPolicyClicked")] PrivacyPolicyClicked = 5,
		[Identifier("ExitClicked")] ExitClicked = 6,
		[Identifier("LevelClicked")] LevelClicked = 7,
		[Identifier("ChangeSceneClicked")] ChangeSceneClicked = 8,
	}

	public static class EventsIdentifierExtension
	{
		public static EventsRecord GetRecord(this EventsIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.EventsModel.GetRecord(identifier, editableRecord);
		}
	}
}
