using UnityEngine;
using UnityEngine.Events;

namespace DragFeature {
    [CreateAssetMenu(menuName = "DragFeature/DragSystem")]
    public class DragSystem : ScriptableObject {
        public bool IsActive {
            get;
            private set;
        }
        public UnityAction<bool> OnChange;

        public UnityAction<GameObject?> OnDrag;

        public void Activate() {
            IsActive = true;
            OnChange.Invoke(true);
        }

        public void Deactivate() {
            IsActive = false;
            OnChange.Invoke(false);
        }
    }
}