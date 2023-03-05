using System.Collections.Generic;



namespace Core {
    public class Gun_2_Tower : ProjectileTower<TestProjectileBehaviour> {
        static int[] _damage = { 30, 35, 40, 45, 50, 55 };
        static float[] _atkRate = { 
            2,
            2.4f,
            2.8f,
            3.2f,
            3.6f,
            4
        };

        static float[] _range = { 3, 3.2f, 3.4f, 3.6f, 3.8f, 4f };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 25, 50, 100, 175, 275);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 25, 50, 100, 175, 275);

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            results.Add(new UpgradeOption(rangeUpgrade, 1));
        }

        protected override float GetAtkDelay() {
            return 1f / _atkRate[spdUpgrade.currentLevel];
        }

        protected override TowerDamageInstance GetDamage() {
            return new TowerDamageInstance(DamageType.normal, _damage[dmgUpgrade.currentLevel]);
        }

        protected override float GetProjectileSpeed() {
            return GetRange() * 5.5f;
        }

        protected override float GetRange() {
            return _range[rangeUpgrade.currentLevel];
        }

        public override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel;
        }

        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>() { dmgUpgrade, spdUpgrade, rangeUpgrade };
        }
    }
}
