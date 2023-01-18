using System.Collections.Generic;

namespace Core {
    public class WallTower : TowerBehaviour {
        public override void GameUpdate(ScenarioInstance s) { }

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) { }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) {
            results.Add(new SpecializationUpgradeOptions(this, TowerDefinitionCatalog.gun_1, 25, 0.1f));
        }
        protected override int GetTotalUpgradeLevel() {
            return 0;
        }
        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>();
        }
    }
}
