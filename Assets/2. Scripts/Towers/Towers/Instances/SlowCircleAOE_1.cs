using System.Collections.Generic;



namespace Core {
    public class SlowCircleAOE_1 : CircleAOE_1 {
        static float[] _slowPower = {
            .7f, 
            .8f, .9f, 1.0f, 1.1f, 1.2f,
            1.3f, 1.4f, 1.5f
        };

        static float[] _slowTime = {
            3, 
            3.5f, 4, 4.5f, 5, 5.5f,
            6, 6.5f, 7
        };

        TowerUpgradeDetails slowUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275, 400, 550, 725);


        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
            base.GetGeneralUpgradeOptions(results);
        }
        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) { }


        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            var result = base.GetTowerUpgradeDetails();
            result.Add(slowUpgrade);
            return result;
        }
        protected override int GetTotalUpgradeLevel() {
            return base.GetTotalUpgradeLevel() + slowUpgrade.currentLevel;
        }

        protected override void DamageCreep(ScenarioInstance s, CreepInstance c) {
            base.DamageCreep(s, c);
            c.ApplySlow(_slowPower[slowUpgrade.currentLevel], _slowTime[slowUpgrade.currentLevel]);
        }
    }
}
