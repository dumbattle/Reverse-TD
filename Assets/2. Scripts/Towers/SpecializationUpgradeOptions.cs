using System;

namespace Core {
    public struct SpecializationUpgradeOptions {
        public ITower current;
        public TowerDefinition upgradeResult;
        public int cost;
        public float weight;

        public SpecializationUpgradeOptions(ITower current, TowerDefinition upgradeResult, int cost, float weight) {
            this.current = current ?? throw new ArgumentNullException(nameof(current));
            this.upgradeResult = upgradeResult ?? throw new ArgumentNullException(nameof(upgradeResult));
            this.cost = cost;
            this.weight = weight;
        }
    }
}
