﻿#if UNITY_EDITOR
using UnityEditor;
using System;

namespace UnityEngineX.EditorTools
{
	public class IndentBlock : IDisposable
	{
		public IndentBlock()
		{
			EditorGUI.indentLevel++;
		}

		public void Dispose()
		{
			EditorGUI.indentLevel--;
		}
	}
}
#endif