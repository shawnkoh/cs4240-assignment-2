using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FurnitureDestroy Event Channel")]
public class FurnitureDestroyEventChannel : ScriptableObject {
    public UnityAction<GameObject> OnFurnitureDestroyRequested;

    public void RaiseEvent(GameObject furniture) {
        Assert.IsNotNull(OnFurnitureDestroyRequested);
        OnFurnitureDestroyRequested.Invoke(furniture);
    }
}