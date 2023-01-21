using System.Collections.Generic;



namespace Core {

    public class Main_Tower_1 : ProjectileTower<TestProjectileBehaviour>, IMainTower {
        bool defeated = false;

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
        }

        public void SetAsDefeated() {
            defeated = true;
            active = false;
        }
        public bool IsDefeated() => defeated;

        protected override float GetAtkDelay() {
            return 1;
        }

        protected override int GetDamage() {
            return 25;
        }

        protected override float GetProjectileSpeed() {
            return 25;
        }

        protected override float GetRange() {
            return 10;
        }

        protected override int GetTotalUpgradeLevel() {
            return 0;
        }
        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>();
        }
    }
}
