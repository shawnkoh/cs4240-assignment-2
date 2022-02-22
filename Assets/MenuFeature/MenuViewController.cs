using System.Collections.Generic;
using AppFeature;
using Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class MenuViewController : MonoBehaviour {
        public Store store;
        private AsyncOperationHandle<IList<Furniture>> _loadHandle;

        private void Start() {
            var root = GetComponent<UIDocument>().rootVisualElement;
            // FIXME: This should be a ListView instead.
            var catalog = root.Q<VisualElement>("catalog");
            _loadHandle = Addressables.LoadAssetsAsync<Furniture>("furniture", furniture => 
                { catalog.Add(new FurnitureButton(store, furniture)); });
        }

        private void OnDestroy() {
            Addressables.Release(_loadHandle);
        }
    }
}