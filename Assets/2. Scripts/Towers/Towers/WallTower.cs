using System.Collections.Generic;


namespace Core {
    public class WallTower : TowerBehaviour {
        public override void GameUpdate(ScenarioInstance s) { }

        public override void GetUpgradeOptions(List<UpgradeOption> results) { }

        protected override int GetTotalUpgradeLevel() {
            return 0;
        }
    }
}
