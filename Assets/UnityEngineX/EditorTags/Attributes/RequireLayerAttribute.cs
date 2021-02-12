using System;

namespace UnityEngineX
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RequireLayerAttribute : Attribute
	{
		public readonly string LayerName;
		public readonly int LayerIndex = -1;

		public RequireLayerAttribute(string layer)
		{
			LayerName = layer;
		}
		
		public RequireLayerAttribute(int layer)
		{
			LayerIndex = layer;
		}
	}
}