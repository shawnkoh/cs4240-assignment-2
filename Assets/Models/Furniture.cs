using UnityEngine;

namespace Models {
    [CreateAssetMenu(menuName = "Models/Furniture")]
    public class Furniture : ScriptableObject {
        public GameObject prefab;
        public Texture2D Thumbnail {
            get
            {
                // NB: Initial size doesn't matter. LoadImage overwrites.
                var texture = new Texture2D(2, 2);
                texture.LoadImage(rawThumbnail);
                return texture;
            }
        }

        public byte[] rawThumbnail;
    }
}