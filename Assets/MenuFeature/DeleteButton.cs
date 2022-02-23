using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class DeleteButton: Button {
        private readonly GameStore _gameStore;
        
        public DeleteButton(GameStore gameStore) {
            _gameStore = gameStore;
            text = "Delete";
            
            _gameStore.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                gameStore.OnChange -= Subscriber;
            });

            clicked += () => _gameStore.DeleteButtonTapped();
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