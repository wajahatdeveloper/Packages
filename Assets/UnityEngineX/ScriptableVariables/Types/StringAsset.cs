using UnityEngine;

[CreateAssetMenu(fileName = "StringAsset", menuName = "Variables/New String Asset")]
public class StringAsset : ScriptableValueAsset<string> {}

[System.Serializable]
public class String_Reference : ScriptableValueReference<string, StringAsset> {}