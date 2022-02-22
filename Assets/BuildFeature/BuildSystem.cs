using System;
using System.Collections.Generic;
using AppFeature;
using GameFeature;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace BuildFeature {
    public class BuildSystem : MonoBehaviour {
        public UnityAction<Pose?> OnRaycast;
        public UnityAction OnFurniturePlaced;
        public Pose? Pose; 
        public Store store;
        private ARRaycastManager _raycastManager;

        private void Awake() {
            _raycastManager = FindObjectOfType<ARRaycastManager>();
            store.OnChange += GameStateSubscriber;
        }

        private void Update() {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
            if (hits.Count == 0) {
                Pose = null;
                OnRaycast.Invoke(null);
                return;
            }

            var hit = hits[0];
            // TODO: Check if location will collide
            // Alternatively, check if hit another Furniture.
            Pose = hit.pose;
            OnRaycast.Invoke(hit.pose);
        }

        private void OnDestroy() {
            store.OnChange -= GameStateSubscriber;
        }

        private void GameStateSubscriber(GameState state) {
            state.Switch(
                idleState => { enabled = false; },
                buildState => { enabled = true; },
                editState => { enabled = false; }
            );
        }

        public void PlaceFurniture(Furniture furniture) {
            if (!Pose.HasValue)
                throw new InvalidOperationException();
            Instantiate(furniture.prefab, Pose.Value.position, Pose.Value.rotation);
            OnFurniturePlaced.Invoke();
        }
    }
}