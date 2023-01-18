using System;
using System.Collections.Generic;


namespace Core {
    public class PlayerData {
        public int money;
        public CreepArmy creepArmy = new CreepArmy();
        public List<IPlayerItem> items = new List<IPlayerItem>();
        public GlobalCreeepUpgrades globalCreeepUpgrades = new GlobalCreeepUpgrades();
        public ShopInstance shop = new ShopInstance();

        public void Init(ScenarioInstance s) {
            shop.Init(s);
        }
    }
}
