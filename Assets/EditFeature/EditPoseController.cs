using System;
using JetBrains.Annotations;
using UnityEngine;

namespace EditFeature {
    public class EditPoseController : MonoBehaviour {
        public EditSystem editSystem;

        private void Awake() {
            editSystem.OnChange += Subscriber;
            Subscriber(editSystem.Furniture);
        }

        private void OnDestroy() {
            editSystem.OnChange -= Subscriber;
        }

        private void Update() {
            if (editSystem.Furniture == null)
                throw new InvalidOperationException();
            // TODO: This needs to be above.
            gameObject.transform.SetPositionAndRotation(editSystem.Furniture.transform.position, editSystem.Furniture.transform.rotation);
        }

        private void Subscriber([CanBeNull] GameObject furniture) {
            if (furniture == null) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
        }
    }
}