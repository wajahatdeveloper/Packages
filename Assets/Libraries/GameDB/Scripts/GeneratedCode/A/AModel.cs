using System;
using UnityEngine;
namespace GameDB
{
	//Generated code, this script is only generated once and will not be overwritted!
	//You can add code to this script to iterate your records but don't remove the generated code!

	[Serializable]
	public class AModel : BaseModel<ARecord, AIdentifier>
	{
		[SerializeField] private ARecord[] records = default;
		protected override ARecord[] Records { get { return records; } }

		//Add your code below this line
	}
}
