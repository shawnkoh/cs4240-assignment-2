using System;
using GameFeature;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace AppFeature {
    [CreateAssetMenu(menuName = "AppFeature/Store")]
    public class Store: ScriptableObject {
        public UnityAction<GameState> OnChange;
        public GameState gameState;

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
                    Instantiate(buildState.Furniture.prefab);
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