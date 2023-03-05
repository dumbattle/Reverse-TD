using System.Collections.Generic;



namespace Core {
    public class CircleAOE_1 : CircleAOETower {
        static int[] _damage = {
            40, 
            47, 54, 61, 68, 75,
            82, 89, 96
        };

        static float[] _atkRate = {
            .5f, 
            .6f, .7f, .8f, .9f, 1f,
            1.1f, 1.2f, 1.3f
        };

        static float[] _range = {
            3, 
            3.1f, 3.2f, 3.3f, 3.4f, 3.5f,
            3.6f, 3.7f, 3.8f
        };
        static float[] _xtraRange = {
            .5f, 
            .6f, .7f, .8f, .9f, 1f,
            1.1f, 1.2f, 1.3f
        };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275, 400, 550, 725);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 25, 50, 100, 175, 275, 400, 550, 725);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 25, 50, 100, 175, 275, 400, 550, 725);
        TowerUpgradeDetails splashUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPLASH, 25, 50, 100, 175, 275, 400, 550, 725);

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            results.Add(new UpgradeOption(rangeUpgrade, 1));
            results.Add(new UpgradeOption(splashUpgrade, 1));
        }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) {
            results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.circleAOE_slow_1, 275, 4));
            results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.circleAOE_2, 275, 4));
        }

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
