using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class CancelButton: Button {
        private readonly GameStore _gameStore;
        
        public CancelButton(GameStore gameStore) {
            _gameStore = gameStore;
            text = "Cancel";

            _gameStore.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                gameStore.OnChange -= Subscriber;
            });

            clicked += () => _gameStore.CancelButtonTapped();
        }

        private void Subscriber(GameState state) {
            state.Switch(
                idleState => SetEnabled(false),
                buildState => SetEnabled(true),
                editState => SetEnabled(true)
            );
        }
    }
}