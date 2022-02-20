using JetBrains.Annotations;
using MyAssets.Models;
using UnityEngine;

namespace MyAssets.MenuFeature.State {
    [CreateAssetMenu]
    public class BuildingState : ScriptableObject {
        [NotNull]
        public Furniture furniture;
    }
}