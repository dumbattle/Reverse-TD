using System.Collections.Generic;


namespace Core {
    public class PlayerInventory {
        public List<IPlayerItem> items = new List<IPlayerItem>();

        public void AddItem(IPlayerItem item) {
            items.Add(item);
        }

        public int NumAttachable(CreepSquad c) {
            var result = 0;
            //foreach (var item in items) {
            //    if (item is CreepAttatchment m) {
            //        result++;
            //    }
            //}
            return result;
        }
    }
}
