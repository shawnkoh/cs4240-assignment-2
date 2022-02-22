using Models;
using OneOf;
using UnityEngine;

namespace AppFeature {
    public class AppState : OneOfBase<IdleState, BuildState, EditState> {
        AppState(OneOf<IdleState, BuildState, EditState> _) : base(_) { }

        // These are just boilerplate code required to use the OneOf package.
        // C# does not support value types yet so this is a necessarily evil.
        public static implicit operator AppState(IdleState _) => new AppState(_);
        public static implicit operator AppState(BuildState _) => new AppState(_);
        public static implicit operator AppState(EditState _) => new AppState(_);
    }

    public struct IdleState {
    }

    public struct BuildState {
        public Furniture Furniture;
        public Vector3? HitPosition;

        public BuildState(Furniture furniture) {
            Furniture = furniture;
            HitPosition = null;
        }
    }

    public struct EditState {
        public GameObject Furniture;
    }
}