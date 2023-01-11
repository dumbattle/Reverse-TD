using System.Collections.Generic;


namespace Core {
    public class PlayerFunctions {
        ScenarioParameters parameters;
        PlayerData player;

        public PlayerFunctions(ScenarioParameters parameters, PlayerData player) {
            this.parameters = parameters;
            this.player = player;
        }

        public void AddMoney(int amnt) {
            player.money += amnt;
            parameters.ui.moneyText.text = player.money.ToString();
        }

        public CreepArmy GetCreepArmy() {
            return player.creepArmy;
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
                if (item is CreepAttatchment m) {
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
