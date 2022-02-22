using AppFeature;
using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class CancelButton: Button {
        private readonly Store _store;
        
        public CancelButton(Store store) {
            _store = store;
            text = "Cancel";

            _store.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                store.OnChange -= Subscriber;
            });

            clicked += () => _store.CancelButtonTapped();
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