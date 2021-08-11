using UnityEngine;

[CreateAssetMenu(fileName = "Vector3Asset", menuName = "Variables/New Vector3 Asset")]
public class Vector3Asset : ScriptableValueAsset<Vector3> {}

[System.Serializable]
public class Vector3_Reference : ScriptableValueReference<Vector3, Vector3Asset> {}