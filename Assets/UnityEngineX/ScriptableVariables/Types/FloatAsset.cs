using UnityEngine;

[CreateAssetMenu(fileName = "FloatAsset", menuName = "Variables/New Float Asset")]
public class FloatAsset : ScriptableValueAsset<float> {}

[System.Serializable]
public class Float_Reference : ScriptableValueReference<float, FloatAsset> {}