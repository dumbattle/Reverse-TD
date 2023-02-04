using System.Collections.Generic;


namespace Core {
    public class PlayerFunctions {
        ScenarioUnityReferences references;
        PlayerData player;

        public PlayerFunctions(ScenarioUnityReferences references, PlayerData player) {
            this.references = references;
            this.player = player;
        }
        public int CurrentMoney() {
            return player.money;
        }
        public void AddMoney(int amnt) {
            player.money += amnt;
            references.ui.moneyText.text = player.money.ToString();
        }
        public CreepArmy GetCreepArmy() {
            return player.creepArmy;
        }
        public GlobalCreeepUpgrades GetGlobalCreeepUpgrades() {
            return player.globalCreeepUpgrades;
        }

        public int NumAttachableInInventory(CreepSquad c) {
            var result = 0; 
            return result;
        }
    }
}
