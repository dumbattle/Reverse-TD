using System;

namespace Core {
    public struct UpgradeOption {
        public TowerUpgradeDetails upgrade;
        public float rawWeight;

        public UpgradeOption(TowerUpgradeDetails upgrade, float rawWeight) {
            this.upgrade = upgrade ?? throw new ArgumentNullException(nameof(upgrade));
            this.rawWeight = rawWeight;
        }
    }

}
