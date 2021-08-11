using UnityEngine;

[CreateAssetMenu(fileName = "IntAsset", menuName = "Variables/New Int Asset")]
public class IntAsset : ScriptableValueAsset<int> {}

[System.Serializable]
public class Int_Reference : ScriptableValueReference<int, IntAsset> {}