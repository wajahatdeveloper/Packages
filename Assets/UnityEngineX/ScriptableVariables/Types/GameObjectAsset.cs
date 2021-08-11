using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectAsset", menuName = "Variables/New GameObject Asset")]
public class GameObjectAsset : ScriptableValueAsset<GameObject> {}

[System.Serializable]
public class GameObject_Reference : ScriptableValueReference<GameObject, GameObjectAsset> {}