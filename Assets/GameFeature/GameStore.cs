using System;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace GameFeature {
    [CreateAssetMenu(menuName = "AppFeature/Store")]
    public class GameStore: ScriptableObject {
        public UnityAction<GameState> OnChange;
        public GameState gameState;

        private void Awake() {
            gameState = new IdleState();
        }

        public void FurnitureButtonTapped(Furniture furniture) {
            gameState.Switch(
                idleState => {
                    gameState = new BuildState(furniture);
                },
                buildState => throw new InvalidOperationException(),
                editState => {
                    gameState = new BuildState(furniture);
                }
            );
            OnChange.Invoke(gameState);
        }

        public void PlaceButtonTapped() {
            gameState.Switch(
                idleState => throw new InvalidOperationException(),
                buildState => {
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