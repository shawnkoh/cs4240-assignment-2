using AppFeature;
using GameFeature;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace BuildIndicatorFeature {
    public class BuildIndicatorController : MonoBehaviour {
        public Store store;
        private ARRaycastManager _raycastManager;

        private void Awake() {
            store.OnChange += Subscriber;
            _raycastManager = FindObjectOfType<ARRaycastManager>();
        }

        private void OnDestroy() {
            store.OnChange -= Subscriber;
        }

        private void Update() {
            // TODO: Get raycast manager to cast
        }

        private void Subscriber(AppState appState) {
            // TODO: Do i need to set enabled if i already set active?
            appState.Switch(
                idleState => {
                    enabled = false;
                    gameObject.SetActive(false);
                },
                buildingState => {
                    enabled = true;
                    gameObject.SetActive(true);
                },
                editingState => {
                    enabled = false;
                    gameObject.SetActive(false);
                }
            );
        }
    }
}
