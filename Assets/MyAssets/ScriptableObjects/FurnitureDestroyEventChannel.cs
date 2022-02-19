using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FurnitureDestroy Event Channel")]
public class FurnitureDestroyEventChannel : ScriptableObject {
    public UnityAction<GameObject> OnFurnitureDestroyRequested;

    public void RaiseEvent(GameObject furniture) {
        if (OnFurnitureDestroyRequested == null) {
            Debug.LogWarning("Attempted to destroy furniture but no listener");
            return;
        }

        OnFurnitureDestroyRequested.Invoke(furniture);
    }
}