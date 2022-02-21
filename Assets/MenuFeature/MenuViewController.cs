using System.Collections.Generic;
using MyAssets.MenuFeature.State;
using MyAssets.Models;
using MyAssets.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class MenuViewController : MonoBehaviour {
    private FurnitureBuildEventChannel _buildEventChannel;
    private FurnitureSpawnEventChannel _spawnEventChannel;
    private AsyncOperationHandle<IList<Furniture>> _loadHandle;

    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        // FIXME: This should be a ListView instead.
        var catalog = root.Q<VisualElement>("catalog");
        _loadHandle = Addressables.LoadAssetsAsync<Furniture>("furniture", furniture => {
            catalog.Add(new FurnitureButton(furniture));
        });
    }
    
    private void OnDestroy() {
        Addressables.Release(_loadHandle);
    }
}
