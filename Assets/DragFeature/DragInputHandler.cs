using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DragFeature {
    public class DragInputHandler : MonoBehaviour {
        public DragSystem DragSystem;
        private ARRaycastManager _raycastManager;

        private void Awake() {
            DragSystem.OnChange += Subscriber;
            _raycastManager = FindObjectOfType<ARRaycastManager>();
        }

        private void OnDestroy() {
            DragSystem.OnChange -= Subscriber;
        }

        private bool _isDraggable;
        [CanBeNull] private GameObject _furniture;

        private void Update() {
            if (Input.touchCount == 0)
                return;
            var touch = Input.GetTouch(0);

            RaycastHit hit;
            var ray = Camera.current.ScreenPointToRay(touch.position);
            var hits = new List<ARRaycastHit>();
            
            if (!_raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                return;
            var raycastHit = hits[0];

            if (touch.phase == TouchPhase.Began) {
                if (!Physics.Raycast(ray, out hit))
                    return;
                if (hit.collider.gameObject.GetComponent<DraggableTag>() == null)
                    return;
                _furniture = hit.collider.gameObject;
                DragSystem.OnDrag.Invoke(_furniture);
                _isDraggable = true;
            } else if (_isDraggable && touch.phase == TouchPhase.Moved && _furniture != null) {
                _furniture.transform.position = raycastHit.pose.position;
            } else if (touch.phase == TouchPhase.Ended) {
                DragSystem.OnDrag.Invoke(null);
                _furniture = null;
                _isDraggable = false;
            }
        }

        private void Subscriber(bool isActive) {
            enabled = isActive;
        }
    }
}