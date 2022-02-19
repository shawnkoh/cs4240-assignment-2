using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class FurnitureSpawnEventChannel : ScriptableObject {
    public UnityAction<GameObject> OnSpawnRequested;

    public void RaiseEvent(GameObject furniture) {
        Assert.IsNotNull(OnSpawnRequested);
        OnSpawnRequested.Invoke(furniture);
    }
}