using System;
using System.Collections.Generic;


namespace Core {
    public class PlayerData {
        public ResourceCollection money = new ResourceCollection();
        public CreepArmy creepArmy = new CreepArmy();
        public GlobalCreeepUpgrades globalCreeepUpgrades = new GlobalCreeepUpgrades();

        public void Init(ScenarioInstance s) {
        }
    }
}
