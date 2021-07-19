namespace SheetCodes
{
	//Generated code, do not edit!

	public enum EventsIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("SettingsClicked")] SettingsClicked = 1,
	}

	public static class EventsIdentifierExtension
	{
		public static EventsRecord GetRecord(this EventsIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.EventsModel.GetRecord(identifier, editableRecord);
		}
	}
}
