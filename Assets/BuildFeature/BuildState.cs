using Models;

namespace BuildFeature {
    public struct BuildState {
        public Furniture Furniture;

        public BuildState(Furniture furniture) {
            Furniture = furniture;
        }
    }
}