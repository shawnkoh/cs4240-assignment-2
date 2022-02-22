using AppFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class CancelButton: Button {
        private readonly Store _store;
        
        public CancelButton(Store store) {
            _store = store;
            
            _store.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                store.OnChange -= Subscriber;
            });

            clicked += () => _store.CancelButtonTapped();
        }

        private void Subscriber(AppState state) {
            state.Switch(
                idleState => SetEnabled(false),
                buildState => SetEnabled(true),
                editState => SetEnabled(true)
            );
        }
    }
}