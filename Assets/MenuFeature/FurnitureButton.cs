using GameFeature;
using Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class FurnitureButton : Button {
        private readonly GameStore _gameStore;
        private readonly Furniture _furniture;

        public FurnitureButton(GameStore gameStore, Furniture furniture) {
            _gameStore = gameStore;
            _furniture = furniture;
            style.backgroundImage = _furniture.thumbnail;
            style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            name = _furniture.name;
            clicked += OnTapped;
            
            gameStore.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                gameStore.OnChange -= Subscriber;
            });
        }

        private void OnTapped() {
            _gameStore.FurnitureButtonTapped(_furniture);
        }

        private void Subscriber(GameState state) {
            state.Switch(
                idleState => SetEnabled(true),
                buildingState => SetEnabled(_furniture == buildingState.Furniture),
                editingState => SetEnabled(false)
            );
        }
    }
}