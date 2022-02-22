using System.Collections.Generic;
using AppFeature;
using GameFeature;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace BuildIndicatorFeature {
    public class BuildSystem : MonoBehaviour {
        public UnityAction<Pose?> OnRaycast;
        public Store store;
        private ARRaycastManager _raycastManager;

        private void Awake() {
            _raycastManager = FindObjectOfType<ARRaycastManager>();
            store.OnChange += Subscriber;
        }

        private void Update() {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
            if (hits.Count == 0) {
                OnRaycast.Invoke(null);
                return;
            }

            var hit = hits[0];
            // TODO: Check if location will collide
            // Alternatively, check if hit another Furniture.
            OnRaycast.Invoke(hit.pose);
        }

        private void OnDestroy() {
            store.OnChange -= Subscriber;
        }

        private void Subscriber(GameState state) {
            state.Switch(
                idleState => { enabled = false; },
                buildState => { enabled = true; },
                editState => { enabled = false; }
            );
        }
    }
}