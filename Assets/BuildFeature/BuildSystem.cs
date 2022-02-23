using System;
using Models;
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
            var placed = Instantiate(BuildState.Value.Furniture.prefab, Pose.Value.position, Pose.Value.rotation);
            var placedBounds = placed.GetComponent<MeshCollider>().bounds;
            var furnitures = FindObjectsOfType<FurnitureTag>();
            foreach (var furnitureTag in furnitures) {
                var obj = furnitureTag.gameObject;
                if (obj == placed)
                    continue;
                if (obj.GetComponent<MeshCollider>().bounds.Intersects(placedBounds)) {
                    Destroy(placed);
                    return;
                }
            }
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