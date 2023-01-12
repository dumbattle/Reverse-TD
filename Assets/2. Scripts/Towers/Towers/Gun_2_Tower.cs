using System.Collections.Generic;



namespace Core {
    public class Gun_2_Tower : ProjectileTower<TestProjectileBehaviour> {
        static int[] _damage = { 20, 23, 26, 33, 36, 40 };
        static float[] _atkRate = { 
            4,
            4.8f,
            5.6f,
            6.4f,
            7.2f,
            8
        };

        static float[] _range = { 3, 3.2f, 3.4f, 3.6f, 3.8f, 4f };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 45, 100, 210, 375, 595);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 30, 60, 120, 210, 340);

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
            return GetRange() * 5.5f;
        }

        protected override float GetRange() {
            return _range[rangeUpgrade.currentLevel];
        }

        protected override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel;
        }

        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>() { dmgUpgrade, spdUpgrade, rangeUpgrade };
        }
    }
}
