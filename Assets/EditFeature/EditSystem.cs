using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EditFeature {
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class EditSystem : ScriptableObject {
        public UnityAction<GameObject?> OnChange;
        
        [CanBeNull]
        public GameObject Furniture {
            get;
            private set;
        }

        public void Activate(GameObject furniture) {
            Furniture = furniture;
            OnChange.Invoke(furniture);
        }

        public void Deactivate() {
            Furniture = null;
            OnChange.Invoke(null);
        }

        public void DestroyFurniture() {
            Destroy(Furniture);
            Furniture = null;
        }
    }
}