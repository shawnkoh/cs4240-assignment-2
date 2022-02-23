using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class PlaceButton: Button {
        private readonly GameStore _gameStore;
        
        public PlaceButton(GameStore gameStore) {
            _gameStore = gameStore;
            text = "Place";
            
            _gameStore.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                gameStore.OnChange -= Subscriber;
            });

            clicked += () => _gameStore.PlaceButtonTapped();
        }

        private void Subscriber(GameState state) {
            state.Switch(
                idleState => SetEnabled(false),
                buildState => SetEnabled(true),
                editState => SetEnabled(false)
            );
        }
    }
}