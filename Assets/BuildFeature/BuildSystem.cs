using System;
using UnityEngine;
using UnityEngine.Events;

namespace BuildFeature {
    [CreateAssetMenu(menuName = "BuildFeature/BuildSystem")]
    public class BuildSystem : ScriptableObject {
        public UnityAction<BuildState?> OnStatusChanged;
        public UnityAction<Pose?> OnPoseChanged;

        public BuildState? BuildState {
            get;
            private set;
        }

        // TODO: We should really try to put pose inside BuildState.
        public Pose? Pose {
            get;
            private set;
        }

        public void SetPose(Pose? pose) {
            Pose = pose;
            OnPoseChanged.Invoke(pose);
        }

        public void PlaceFurniture() {
            if (!Pose.HasValue && !BuildState.HasValue)
                throw new InvalidOperationException();
            Instantiate(BuildState.Value.Furniture.prefab, Pose.Value.position, Pose.Value.rotation);
        }

        public void Activate(BuildState buildState) {
            BuildState = buildState;
            OnStatusChanged.Invoke(buildState);
        }

        public void Deactivate() {
            BuildState = null;
            Pose = null;
            OnStatusChanged.Invoke(null);
            OnPoseChanged.Invoke(null);
        }
    }
}