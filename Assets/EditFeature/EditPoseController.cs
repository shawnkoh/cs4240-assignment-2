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
            var position = editSystem.Furniture.transform.position;
            var offset = editSystem.Furniture.GetComponent<MeshRenderer>().bounds.extents.y + gameObject.GetComponentInChildren<MeshRenderer>().bounds.extents.y;
            var y = position.y + offset;
            var indicatorPosition = new Vector3(position.x, y, position.z);
            gameObject.transform.position = indicatorPosition;

            var g = GameObject.Find("rack_100");
            gameObject.transform.position = g.transform.position;
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
