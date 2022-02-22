using UnityEngine;

namespace Models {
    [CreateAssetMenu(menuName = "Models/Furniture")]
    public class Furniture : ScriptableObject {
        [SerializeField]
        public GameObject prefab;
        [SerializeField]
        public Texture2D image;
    }
}