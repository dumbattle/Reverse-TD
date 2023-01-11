using System.Collections.Generic;


namespace Core {
    public class PlayerData {
        public int money;
        public CreepArmy creepArmy = new CreepArmy();
        public List<IPlayerItem> items = new List<IPlayerItem>();
    }
}
