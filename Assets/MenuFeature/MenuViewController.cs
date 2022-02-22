using System.Collections.Generic;
using AppFeature;
using GameFeature;
using Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class MenuViewController : MonoBehaviour {
        public Store store;
        private AsyncOperationHandle<IList<Furniture>> _loadHandle;
        
        private VisualElement _buttonContainer;

        private void Start() {
            var root = GetComponent<UIDocument>().rootVisualElement;
            // FIXME: This should be a ListView instead.
            var catalog = root.Q<VisualElement>("catalog");
            _loadHandle = Addressables.LoadAssetsAsync<Furniture>("furniture", furniture => 
                { catalog.Add(new FurnitureButton(store, furniture)); });
            _buttonContainer = root.Q<VisualElement>("buttonContainer");
            store.OnChange += Subscriber;
        }

        private void OnDestroy() {
            Addressables.Release(_loadHandle);
            store.OnChange -= Subscriber;
        }

        private void Subscriber(GameState state) {
            // TODO: This will keep deleting and recreating.
            _buttonContainer.Clear();
            state.Switch(
                idleState => { },
                buildState => {
                    _buttonContainer.Add(new PlaceButton(store));
                    _buttonContainer.Add(new CancelButton(store));
                },
                editState => {
                    _buttonContainer.Add(new DeleteButton(store));
                    _buttonContainer.Add(new CancelButton(store));
                }
            );
        }
    }
}