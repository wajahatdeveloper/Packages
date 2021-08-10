namespace GameDB
{
	//Generated code, do not edit!

	public enum AIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("s")] S = 1,
	}

	public static class AIdentifierExtension
	{
		public static ARecord GetRecord(this AIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.AModel.GetRecord(identifier, editableRecord);
		}
	}
}
