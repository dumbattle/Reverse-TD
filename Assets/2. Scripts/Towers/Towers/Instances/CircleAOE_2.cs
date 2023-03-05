using System.Collections.Generic;



namespace Core {
    public class CircleAOE_2 : CircleAOETower {
        static int[] _damage = {
            30, 
            36, 42, 48, 54, 60,
            66, 72, 78, 84, 90
        };

        static float[] _atkRate = {
            1.5f,
            1.6f, 1.7f, 1.8f, 1.9f, 2f,
            2.1f, 2.2f, 2.3f, 2.4f, 2.5f
        };

        static float[] _range = {
            3, 
            3.1f, 3.2f, 3.3f, 3.4f, 3.5f,
            3.6f, 3.7f, 3.8f, 3.9f, 4f
        };

        static float[] _xtraRange = {
            .5f,
            .6f, .7f, .8f, .9f, 1f,
            1.1f, 1.2f, 1.3f, 1.4f, 1.5f
        };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275, 400, 550, 725, 925, 1150);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 25, 50, 100, 175, 275, 400, 550, 725, 925, 1150);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 25, 50, 100, 175, 275, 400, 550, 725, 925, 1150);
        TowerUpgradeDetails splashUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPLASH, 25, 50, 100, 175, 275, 400, 550, 725, 925, 1150);

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            results.Add(new UpgradeOption(rangeUpgrade, 1));
            results.Add(new UpgradeOption(splashUpgrade, 1));
        }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) { }

        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>() { dmgUpgrade, spdUpgrade, rangeUpgrade, splashUpgrade };
        }

        public override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel + splashUpgrade.currentLevel;
        }

        protected override float GetAtkDelay() {
            return 1f / _atkRate[spdUpgrade.currentLevel];
        }

        protected override float GetRange() {
            return _range[rangeUpgrade.currentLevel];
        }


        protected override float GetExtraRange() {
            return _xtraRange[splashUpgrade.currentLevel];
        }

        protected override TowerDamageInstance GetDamage() {
            return new TowerDamageInstance(DamageType.normal, _damage[dmgUpgrade.currentLevel]);
        }
    }
}
