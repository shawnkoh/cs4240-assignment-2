using AppFeature;
using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class PlaceButton: Button {
        private readonly Store _store;
        
        public PlaceButton(Store store) {
            _store = store;
            text = "Place";
            
            _store.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                store.OnChange -= Subscriber;
            });

            clicked += () => _store.PlaceButtonTapped();
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