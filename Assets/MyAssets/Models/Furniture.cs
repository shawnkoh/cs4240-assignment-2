using UnityEngine;

namespace MyAssets.Models {
    [CreateAssetMenu(menuName = "Models/Furniture")]
    public class Furniture : ScriptableObject {
        public GameObject prefab;
        public Texture2D image;
    }
}