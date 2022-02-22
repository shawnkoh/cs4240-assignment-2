using System;
using Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.Exceptions;

namespace AppFeature {
    [CreateAssetMenu]
    public class Store: ScriptableObject {
        public UnityAction<AppState> OnChange;
        public AppState AppState;

        public void FurnitureButtonTapped(Furniture furniture) {
            AppState.Switch(
                idleState => {
                    AppState = new BuildState(furniture);
                },
                buildState => throw new InvalidOperationException(),
                editState => {
                    AppState = new BuildState(furniture);
                }
            );
            OnChange.Invoke(AppState);
        }

        public void PlaceButtonTapped() {
            AppState.Switch(
                idleState => throw new InvalidOperationException(),
                buildState => {
                    Instantiate(buildState.Furniture.prefab);
                    AppState = new IdleState();
                },
                editState => throw new InvalidOperationException()
            );
            OnChange.Invoke(AppState);
        }

        public void CancelButtonTapped() {
            AppState.Switch(
                idleState => throw new InvalidOperationException(),
                buildState => {
                    AppState = new IdleState();
                },
                editState => {
                    AppState = new IdleState();
                }
            );
            OnChange.Invoke(AppState);
        }

        public void DeleteButtonTapped() {
            AppState.Switch(
                state => throw new InvalidOperationException(),
                state => throw new InvalidOperationException(),
                editState => {
                    Destroy(editState.Furniture);
                    AppState = new IdleState();
                }
            );
            OnChange.Invoke(AppState);
        }
    }
}