using System;
using NaughtyAttributes;
using Sisus;
using Sisus.Attributes;
using UnityEngine;

namespace UnityEngineX
{
	public class Note : MonoBehaviour
	{
		[ResizableTextArea]
		public string text = "Type your note here";
	}
}