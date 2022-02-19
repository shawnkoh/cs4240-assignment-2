using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FurnitureSpawnEventChannel : ScriptableObject {
    public UnityAction<GameObject> OnSpawnRequested;

    public void RaiseEvent(GameObject furniture) {
        if (OnSpawnRequested == null) {
            Debug.LogWarning("FurnitureSpawnEventChannel: No listeners");
            return;
        }

        OnSpawnRequested.Invoke(furniture);
    }
}