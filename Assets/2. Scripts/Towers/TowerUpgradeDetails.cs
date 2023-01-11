namespace Core {
    public class TowerUpgradeDetails {
        public string ID;
        public int[] costs;
        public int currentLevel;

        public TowerUpgradeDetails(string ID, params int[] costs) {
            this.ID = ID;
            this.costs = costs;
            currentLevel = 0;
        }

        public bool UpgradeAvailable() {
            return currentLevel < costs.Length;
        }

        public int CurrentUpgradeCost() {
            return UpgradeAvailable() ? costs[currentLevel] : -1;
        }

        public void IncrmentLevel() {
            if (UpgradeAvailable()) {
                currentLevel++;
            }
        }
    }

}
