using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Publishers {
    [CreateAssetMenu(menuName = "Publishers/Void")]
    public class VoidPublisher : ScriptableObject {
        public UnityAction OnEventRaised;

        public void RaiseEvent() {
            if (OnEventRaised == null)
                Assert.IsNotNull(OnEventRaised);
            OnEventRaised.Invoke();
        }
    }
}