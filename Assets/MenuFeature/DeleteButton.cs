using AppFeature;
using GameFeature;
using UnityEngine.UIElements;

namespace MenuFeature {
    public class DeleteButton: Button {
        private readonly Store _store;
        
        public DeleteButton(Store store) {
            _store = store;
            text = "Delete";
            
            _store.OnChange += Subscriber;
            RegisterCallback<DetachFromPanelEvent>(_ => {
                store.OnChange -= Subscriber;
            });

            clicked += () => _store.DeleteButtonTapped();
        }

        private void Subscriber(AppState state) {
            state.Switch(
                idleState => SetEnabled(false),
                buildState => SetEnabled(true),
                editState => SetEnabled(false)
            );
        }
    }
}