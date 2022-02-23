using System.Collections.Generic;
using GameFeature;
using Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class MenuViewController : MonoBehaviour {
        public GameStore gameStore;
        private AsyncOperationHandle<IList<Furniture>> _loadHandle;
        
        private VisualElement _buttonContainer;

        private void Awake() {
            var root = GetComponent<UIDocument>().rootVisualElement;
            // FIXME: This should be a ListView instead.
            var catalog = root.Q<VisualElement>("catalog");
            _loadHandle = Addressables.LoadAssetsAsync<Furniture>("furniture", furniture => 
                { catalog.Add(new FurnitureButton(gameStore, furniture)); });
            _buttonContainer = root.Q<VisualElement>("buttonContainer");
            gameStore.OnChange += Subscriber;
        }

        private void OnDestroy() {
            Addressables.Release(_loadHandle);
            gameStore.OnChange -= Subscriber;
        }

        private void Subscriber(GameState state) {
            // TODO: This is inefficient because it does not check
            // if the state's type changed.
            _buttonContainer.Clear();
            state.Switch(
                idleState => { },
                buildState => {
                    _buttonContainer.Add(new PlaceButton(gameStore));
                    _buttonContainer.Add(new CancelButton(gameStore));
                },
                editState => {
                    _buttonContainer.Add(new DeleteButton(gameStore));
                    _buttonContainer.Add(new CancelButton(gameStore));
                }
            );
        }
    }
}