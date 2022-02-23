using System;
using BuildFeature;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace GameFeature {
    [CreateAssetMenu(menuName = "AppFeature/Store")]
    public class GameStore: ScriptableObject {
        public UnityAction<GameState> OnChange;
        public GameState gameState;
        public BuildSystem buildSystem;

        private void Awake() {
            // TODO: Implement discarding.
            gameState ??= new IdleState();
        }

        public void FurnitureButtonTapped(Furniture furniture) {
            gameState.Switch(
                idleState => {
                    var buildState = new BuildState(furniture);
                    gameState = buildState;
                    buildSystem.Activate(buildState);
                },
                buildState => throw new InvalidOperationException(),
                editState => {
                    var buildState = new BuildState(furniture);
                    gameState = buildState;
                    buildSystem.Activate(buildState);
                }
            );
            OnChange.Invoke(gameState);
        }

        public void PlaceButtonTapped() {
            gameState.Switch(
                idleState => throw new InvalidOperationException(),
                buildState => {
                    buildSystem.PlaceFurniture();
                    buildSystem.Deactivate();
                    gameState = new IdleState();
                },
                editState => throw new InvalidOperationException()
            );
            OnChange.Invoke(gameState);
        }

        public void CancelButtonTapped() {
            gameState.Switch(
                idleState => throw new InvalidOperationException(),
                buildState => {
                    buildSystem.Deactivate();
                    gameState = new IdleState();
                },
                editState => {
                    gameState = new IdleState();
                }
            );
            OnChange.Invoke(gameState);
        }

        public void DeleteButtonTapped() {
            gameState.Switch(
                state => throw new InvalidOperationException(),
                state => throw new InvalidOperationException(),
                editState => {
                    Destroy(editState.Furniture);
                    gameState = new IdleState();
                }
            );
            OnChange.Invoke(gameState);
        }
    }
}