using AppFeature;
using Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class FurnitureButton : Button {
        private readonly Store _store;
        private readonly Furniture _furniture;

        public FurnitureButton(Store store, Furniture furniture) {
            _store = store;
            _furniture = furniture;
            style.backgroundImage = _furniture.image;
            style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            name = _furniture.name;
            clicked += OnTapped;
            
            store.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                store.OnChange -= Subscriber;
            });
        }

        private void OnTapped() {
            _store.FurnitureButtonTapped(_furniture);
        }

        private void Subscriber(AppState state) {
            state.Switch(
                idleState => SetEnabled(true),
                buildingState => SetEnabled(_furniture == buildingState.Furniture),
                editingState => SetEnabled(false)
            );
        }
    }
}