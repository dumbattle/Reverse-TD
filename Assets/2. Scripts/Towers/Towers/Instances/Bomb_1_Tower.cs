using System.Collections.Generic;



namespace Core {
    public class Bomb_1_Tower : ProjectileTower<BombProjectileBehaviour> {
        static int[] _damage = { 
            60, 70, 80, 90, 
            100, 110
        };
        
        static float[] _atkRate = {
            .5f, .55f, .6f, .65f, 
            .7f, .75f
        };

        static float[] _range = {
            4, 4.4f, 4.8f, 5.2f,
            5.6f, 6f
        };
        static float[] _splashRadius = {
            .5f, .7f, .9f, 1.1f,
            1.3f, 1.5f
        };
        static float[] _splashScale = {
            .1f, .25f, .4f, .55f,
            .7f, .85f
        };

        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 25, 50, 100, 175, 275);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 25, 50, 100, 175, 275);
        TowerUpgradeDetails splashUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPLASH, 25, 50, 100, 175, 275);

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            results.Add(new UpgradeOption(rangeUpgrade, 1));
            results.Add(new UpgradeOption(splashUpgrade, 1));
        }

        protected override float GetAtkDelay() {
            return 1f / _atkRate[spdUpgrade.currentLevel];
        }

        protected override TowerDamageInstance GetDamage() {
            return new TowerDamageInstance(DamageType.normal, _damage[dmgUpgrade.currentLevel]);
        }

        protected override float GetProjectileSpeed() {
            return GetRange() * 3f;
        }

        protected override float GetRange() {
            return _range[rangeUpgrade.currentLevel];
        }

        public override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel + splashUpgrade.currentLevel;
        }
        
        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>() { dmgUpgrade, spdUpgrade, rangeUpgrade, splashUpgrade };
        }

        protected override void SetProjectile(BombProjectileBehaviour proj) {
            base.SetProjectile(proj);
            proj.InitSplash(_splashRadius[splashUpgrade.currentLevel], _splashScale[splashUpgrade.currentLevel]);
        }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) {
            results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.circleAOE_1, 275, 4));
        }

    }
}
