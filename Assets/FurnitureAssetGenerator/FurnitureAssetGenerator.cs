using Models;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace FurnitureAssetGeneratorFeature {
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
            var thumbnail = AssetPreview.GetAssetPreview(prefab);
            var rawThumbnail = thumbnail.EncodeToPNG();
            var furniture = ScriptableObject.CreateInstance<Furniture>();
            furniture.rawThumbnail = rawThumbnail;
            furniture.prefab = prefab;
            if (!prefab.GetComponent<FurnitureTag>())
                prefab.AddComponent<FurnitureTag>();
            var path = "Assets/FurnitureAssetGenerator/FurnitureAssets/" + prefab.name + ".asset";
            AssetDatabase.CreateAsset(furniture, path);
        }
    }
}
#endif