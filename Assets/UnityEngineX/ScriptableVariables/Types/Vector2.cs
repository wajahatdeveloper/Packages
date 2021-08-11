using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Asset", menuName = "Variables/New Vector2 Asset")]
public class Vector2Asset : ScriptableValueAsset<Vector2> {}

[System.Serializable]
public class Vector2_Reference : ScriptableValueReference<Vector2, Vector2Asset> {}