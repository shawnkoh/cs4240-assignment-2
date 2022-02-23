using UnityEngine;

namespace BuildFeature {
    public class PoseController : MonoBehaviour {
        public BuildSystem buildSystem;

        private void Awake() {
            buildSystem.OnPoseChanged += Subscriber;
            Subscriber(buildSystem.Pose);
        }

        private void OnDestroy() {
            buildSystem.OnPoseChanged -= Subscriber;
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
