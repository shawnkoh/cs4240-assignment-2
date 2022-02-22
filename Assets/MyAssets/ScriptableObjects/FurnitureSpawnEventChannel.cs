using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace MyAssets.ScriptableObjects {
    public class FurnitureSpawnEventChannel : ScriptableObject {
        public UnityAction<GameObject> OnSpawnRequested;

        public void RaiseEvent(GameObject furniture) {
            Assert.IsNotNull(OnSpawnRequested);
            OnSpawnRequested.Invoke(furniture);
        }
    }
}