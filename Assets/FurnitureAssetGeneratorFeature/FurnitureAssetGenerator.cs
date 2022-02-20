using MyAssets.Models;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyAssets.FurnitureAssetGeneratorFeature {
    public class FurnitureAssetGenerator : MonoBehaviour {
        [Tooltip("These are prefabs that will NOT be in the game. They are only used in the editor to generate Furniture Assets")]
        public GameObject[] furniturePrefabs;

        [ContextMenu("Generate Furniture Assets")]
        private void GenerateFurnitureAssets() {
            foreach (var prefab in furniturePrefabs) {
                GenerateFurnitureAsset(prefab);
            }
        }

        private void GenerateFurnitureAsset(GameObject prefab) {
            var thumbnail = AssetPreview.GetMiniThumbnail(prefab);
            var furniture = ScriptableObject.CreateInstance<Furniture>();
            prefab.tag = "Spawnable";
            furniture.image = thumbnail;
            furniture.prefab = prefab;
            var path = "Assets/FurnitureAssetGeneratorFeature/FurnitureAssets/" + prefab.name + ".asset";
            AssetDatabase.CreateAsset(furniture, path);
        }
    }
}
#endif