using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyAssets.Scripts {
    public class FurnitureButton : MonoBehaviour {
        public GameObject prefab;
        private Button _button;
        private FurnitureSpawnEventChannel _spawnEventChannel;
        private FurnitureDestroyEventChannel _destroyEventChannel;

        private void Start() {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.Q<Button>("name");
        }
    }
}