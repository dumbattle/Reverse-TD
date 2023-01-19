using System.Collections.Generic;



namespace Core {
    public interface IMainTower : ITower {
        bool IsDefeated();
        void SetAsDefeated();
    }
    public class Main_Tower_1 : ProjectileTower<TestProjectileBehaviour>, IMainTower {
        bool defeated = false;

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
        }

        public override void GameUpdate(ScenarioInstance s) {
            if (defeated) {
                return;
            }

            base.GameUpdate(s);
        }

        public void SetAsDefeated() {
            defeated = true;
            print("A");
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
