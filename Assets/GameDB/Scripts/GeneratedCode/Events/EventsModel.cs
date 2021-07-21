using System;
using UnityEngine;
namespace GameDB
{
	//Generated code, this script is only generated once and will not be overwritted!
	//You can add code to this script to iterate your records but don't remove the generated code!

	[Serializable]
	public class EventsModel : BaseModel<EventsRecord, EventsIdentifier>
	{
		[SerializeField] private EventsRecord[] records = default;
		protected override EventsRecord[] Records { get { return records; } }

		//Add your code below this line
	}
}