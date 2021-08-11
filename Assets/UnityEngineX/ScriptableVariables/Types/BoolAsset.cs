using UnityEngine;

[CreateAssetMenu(fileName = "BoolAsset", menuName = "Variables/New Bool Asset")]
public class BoolAsset : ScriptableValueAsset<bool> {}

[System.Serializable]
public class Bool_Reference : ScriptableValueReference<bool, BoolAsset> {}