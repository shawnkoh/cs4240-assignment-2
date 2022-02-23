using UnityEngine;

namespace EditFeature {
    public class EditInputHandler : MonoBehaviour {
        public EditSystem editSystem;

        private void Awake() {
            editSystem.OnChange += Subscriber;
        }

        private void OnDestroy() {
            editSystem.OnChange -= Subscriber;
        }

        private void Update() {
            // TODO: Check raycast
        }

        private void Subscriber(GameObject furniture) {
            if (furniture != gameObject) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
        }
    }
}
