using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace BuildFeature {
    public class BuildRaycaster : MonoBehaviour {
        public BuildSystem buildSystem;
        private ARRaycastManager _raycastManager;

        private void Awake() {
            _raycastManager = FindObjectOfType<ARRaycastManager>();
            buildSystem.OnStatusChanged += Subscriber;
            Subscriber(buildSystem.BuildState);
        }

        private void OnDestroy() {
            buildSystem.OnStatusChanged -= Subscriber;
        }

        private void Update() {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
            if (hits.Count == 0) {
                buildSystem.Pose = null;
                return;
            }

            var hit = hits[0];
            // TODO: Check if location will collide
            // Alternatively, check if hit another Furniture.
            buildSystem.Pose = hit.pose;
        }

        private void Subscriber(BuildState? state) {
            if (!state.HasValue) {
                enabled = false;
                return;
            }
            enabled = true;
        }
    }
}