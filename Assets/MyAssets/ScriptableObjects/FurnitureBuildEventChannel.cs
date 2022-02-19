using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace MyAssets.ScriptableObjects {
    [CreateAssetMenu(menuName = "Build Event Channel")]
    public class FurnitureBuildEventChannel : ScriptableObject {
        public UnityAction<GameObject> OnBuildRequested;

        public void RaiseEvent(GameObject furniture) {
            Assert.IsNotNull(OnBuildRequested);
            OnBuildRequested.Invoke(furniture);
        }
    }
}