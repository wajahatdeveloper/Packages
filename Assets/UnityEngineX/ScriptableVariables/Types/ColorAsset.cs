using UnityEngine;

[CreateAssetMenu(fileName = "ColorAsset", menuName = "Variables/New Color Asset")]
public class ColorAsset : ScriptableValueAsset<Color> {}

[System.Serializable]
public class Color_Reference : ScriptableValueReference<Color, ColorAsset> {}