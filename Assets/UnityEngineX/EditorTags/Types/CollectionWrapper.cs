using System;
using UnityEngineX.Internal;

namespace UnityEngineX
{
	[Serializable]
	public class CollectionWrapper<T> : CollectionWrapperBase
	{
		public T[] Value;
	}
}

namespace UnityEngineX.Internal
{
	[Serializable]
	public class CollectionWrapperBase {}
}

#if UNITY_EDITOR
namespace UnityEngineX.Internal
{
	using UnityEditor;
	using UnityEngine;
	
	[CustomPropertyDrawer(typeof(CollectionWrapperBase), true)]
	public class CollectionWrapperDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var collection = property.FindPropertyRelative("Value");
			return EditorGUI.GetPropertyHeight(collection, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var collection = property.FindPropertyRelative("Value");
			EditorGUI.PropertyField(position, collection, label, true);
		}
	}
}
#endif