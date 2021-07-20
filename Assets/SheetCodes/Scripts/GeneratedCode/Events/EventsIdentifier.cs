namespace SheetCodes
{
	//Generated code, do not edit!

	public enum EventsIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("SettingsClicked")] SettingsClicked = 1,
		[Identifier("PlayClicked")] Playclicked = 2,
		[Identifier("MoreGamesClicked")] Moregamesclicked = 3,
		[Identifier("RateUsClicked")] Rateusclicked = 4,
		[Identifier("PrivacyPolicyClicked")] Privacypolicyclicked = 5,
		[Identifier("ExitClicked")] Exitclicked = 6,
		[Identifier("LevelClicked")] Levelclicked = 7,
	}

	public static class EventsIdentifierExtension
	{
		public static EventsRecord GetRecord(this EventsIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.EventsModel.GetRecord(identifier, editableRecord);
		}
	}
}
