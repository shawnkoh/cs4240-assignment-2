using UnityEngine;

namespace BuildFeature {
    public class PoseController : MonoBehaviour {
        public BuildSystem buildSystem;

        private void Awake() {
            buildSystem.OnStatusChanged += StatusSubscriber;
            StatusSubscriber(buildSystem.BuildState);
        }

        private void OnDestroy() {
            buildSystem.OnStatusChanged -= StatusSubscriber;
        }

        private void Update() {
            // TODO: This should use BuildSystem.OnChange instead
            Subscriber(buildSystem.Pose);
        }

        private void StatusSubscriber(BuildState? state) {
            if (!state.HasValue) {
                enabled = false;
                return;
            }

            enabled = true;
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
