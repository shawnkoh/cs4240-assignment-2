using System;
using BuildFeature;
using DragFeature;
using EditFeature;
using JetBrains.Annotations;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace GameFeature {
    [CreateAssetMenu(menuName = "AppFeature/Store")]
    public class GameStore: ScriptableObject {
        public UnityAction<GameState> OnChange;
        public GameState gameState;
        public BuildSystem buildSystem;
        public EditSystem editSystem;
        public DragSystem dragSystem;

        private void Awake() {
            // TODO: Implement discarding.
            gameState ??= new IdleState();
            dragSystem.OnDrag += DragSubscriber;
            OnChange += Subscriber;
        }

        private void Subscriber(GameState state) {
            state.Switch(
                idleState => dragSystem.Activate(),
                buildState => dragSystem.Deactivate(),
                editState => dragSystem.Activate()
            );
        }

        private void OnDestroy() {
            dragSystem.OnDrag -= DragSubscriber;
            OnChange -= Subscriber;
        }

        private void DragSubscriber([CanBeNull] GameObject gameObject) {
            if (gameObject == null) {
                gameState.Switch(
                    idleState => { },
                    buildState => throw new InvalidOperationException(),
                    editState => { }
                );
                return;
            }
            
            gameState.Switch(
                idleState => {
                    editSystem.Activate(gameObject);
                    gameState = new EditState(gameObject);
                    OnChange.Invoke(gameState);
                },
                buildState => throw new InvalidOperationException(),
                editState => { }
            );
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
                    editSystem.Deactivate();
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
                    editSystem.Deactivate();
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
                    editSystem.DestroyFurniture();
                    editSystem.Deactivate();
                    gameState = new IdleState();
                }
            );
            OnChange.Invoke(gameState);
        }
    }
}