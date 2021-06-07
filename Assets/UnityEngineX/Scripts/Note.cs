using System;
using Sisus;
using Sisus.Attributes;
using UnityEngine;

namespace UnityEngineX
{
	public class Note : MonoBehaviour
	{
		[UseDrawer("StyledTextDrawer"), TextArea]
		public string text = "Type your note here";
	}
}