using System.Collections.Generic;


namespace Core {
    public class PlayerFunctions {
        ScenarioUnityReferences references;
        PlayerData player;
        ResourceCollection newCreepCost = new ResourceCollection();


        public PlayerFunctions(ScenarioUnityReferences references, PlayerData player) {
            this.references = references;
            this.player = player;
        }
        public ResourceAmount GetCurrentResources() {
            return player.money;
        }
        public void Spend(ResourceAmount cost) {
            player.money.Spend(cost);
            RedrawMoneyUI();
        }
        public void AddMoney(ResourceType type, float amnt) {
            player.money[type] += amnt;
            RedrawMoneyUI();
        }
        public void AddMoney(ResourceAmount amnt, float multiplier=1) {
            player.money[ResourceType.green] += amnt[ResourceType.green] * multiplier;
            player.money[ResourceType.red] += amnt[ResourceType.red] * multiplier;
            player.money[ResourceType.blue] += amnt[ResourceType.blue] * multiplier;
            player.money[ResourceType.yellow] += amnt[ResourceType.yellow] * multiplier;
            player.money[ResourceType.diamond] += amnt[ResourceType.diamond] * multiplier;
            RedrawMoneyUI();
        }

        public bool TrySpend(ResourceAmount cost) {
            if (!player.money.Satisfies(cost)) {
                return false;
            }
            Spend(cost);
            return true;
        }

        public CreepArmy GetCreepArmy() {
            return player.creepArmy;
        }
        public ResourceAmount GetNewCreepCost() {
            newCreepCost.Reset();
            newCreepCost[ResourceType.green] = 100 + (player.creepArmy.count - 1) * 25;
            return newCreepCost;

        }
        public GlobalCreeepUpgrades GetGlobalCreeepUpgrades() {
            return player.globalCreeepUpgrades;
        }

        public int NumAttachableInInventory(CreepSquad c) {
            var result = 0; 
            return result;
        }


        void RedrawMoneyUI() {
            references.ui.gMoneyText.text = ((int)player.money[ResourceType.green]).ToString();
            references.ui.rMoneyText.text = ((int)player.money[ResourceType.red]).ToString();
            references.ui.bMoneyText.text = ((int)player.money[ResourceType.blue]).ToString();
            references.ui.yMoneyText.text = ((int)player.money[ResourceType.yellow]).ToString();
            references.ui.dMoneyText.text = ((int)player.money[ResourceType.diamond]).ToString();
        }
    }
}
