using System;
using Models;
using OneOf;
using UnityEngine;

namespace GameFeature {
    [Serializable]
    public class GameState : OneOfBase<IdleState, BuildState, EditState> {
        GameState(OneOf<IdleState, BuildState, EditState> _) : base(_) { }

        // These are just boilerplate code required to use the OneOf package.
        // C# does not support value types yet so this is a necessarily evil.
        public static implicit operator GameState(IdleState _) => new GameState(_);
        public static implicit operator GameState(BuildState _) => new GameState(_);
        public static implicit operator GameState(EditState _) => new GameState(_);
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