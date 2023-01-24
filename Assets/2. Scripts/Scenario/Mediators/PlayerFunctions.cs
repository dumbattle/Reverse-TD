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
        public ShopInstance GetShop() {
            return player.shop;
        }
        public CreepArmy GetCreepArmy() {
            return player.creepArmy;
        }
        public GlobalCreeepUpgrades GetGlobalCreeepUpgrades() {
            return player.globalCreeepUpgrades;
        }

        public void AddItem(IPlayerItem item) {
            player.items.Add(item);
        }
        public void RemoveItem(IPlayerItem item) {
            player.items.Remove(item);
        }
        public int NumAttachableInInventory(CreepSquad c) {
            var result = 0;
            foreach (var item in player.items) {
                if (item is CreepAttatchment m && m.Attachable(c)) {
                    result++;
                }
            }
            return result;
        }
        public int GetPlayerInventoryCount() {
            return player.items.Count;
        }

        public IPlayerItem GetPlayerItemBySlot(int slot) {
            return player.items[slot];
        }
    }
}
