using System;
using AppFeature;
using GameFeature;
using UnityEngine;
using UnityEngine.Events;

namespace BuildIndicatorFeature {
    public class PoseController : MonoBehaviour {
        public BuildSystem buildSystem;

        private void Awake() {
            buildSystem.OnRaycast += Subscriber;
        }

        private void OnDestroy() {
            buildSystem.OnRaycast -= Subscriber;
        }

        private void Subscriber(Pose? pose) {
            if (!pose.HasValue) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(pose.Value.position, Quaternion.identity);
        }
    }
}
