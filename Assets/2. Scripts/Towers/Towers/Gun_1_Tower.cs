using System.Collections.Generic;



namespace Core {
    public class Gun_1_Tower : ProjectileTower<TestProjectileBehaviour> {
        static int[] _damage = { 35, 40, 45, 50 };
        static float[] _atkRate = { 1, 1.33f, 1.66f, 2 };
        static float[] _range = { 3, 3.3f, 3.6f, 4f };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 45, 100, 210);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 30, 60, 120);

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            results.Add(new UpgradeOption(rangeUpgrade, 1));
        }

        protected override float GetAtkDelay() {
            return 1f / _atkRate[spdUpgrade.currentLevel];
        }

        protected override int GetDamage() {
            return _damage[dmgUpgrade.currentLevel];
        }

        protected override float GetProjectileSpeed() {
            return GetRange() * 5f;
        }

        protected override float GetRange() {
            return _range[rangeUpgrade.currentLevel];
        }

        protected override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel;
        }
        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) {
            if (GetTotalUpgradeLevel() >= 4) {
                results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.gun_2, 150, 2));
                results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.bomb_1, 150, 2));
            }
        }

        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>() { dmgUpgrade, spdUpgrade, rangeUpgrade };
        }
    }
}
