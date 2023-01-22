using UnityEngine;


namespace Core {
    public static class PlayerItemUtility {

        static Entry[] allItems = {
            new Entry(new BasicCreepAttachment_Count(), 100, 0),
            new Entry(new BasicCreepAttachment_HP(), 100, 0),
            new Entry(new BasicCreepAttachment_SPD(), 100, 0),
            new Entry(new BasicCreepAttachment_Money(), 50, 0),
            new Entry(new BasicCreepAttachment_SpawnRate(), 100, 0),
            new Entry(new CreepAttachment_SpeedGreedy(), 10000, 0),

            new Entry(new BasicCreepAttachment_Regen(), 100, 0),
            new Entry(new StatShiftCreepAttachment_Tank(), 10, 0),
            new Entry(new StatShiftCreepAttachment_Speed(), 10, 0),
            new Entry(new StatShiftCreepAttachment_Swarm(), 10, 0),
        };

        public static IPlayerItem GetRandomItem(int round) {
            Entry result = null;

            float totalWeight = 0;

            foreach (var e in allItems) {
                if (e.unlockRound > round) {
                    continue;
                }

                var w = e.weight;
                totalWeight += w;

                if (Random.value < w / totalWeight) {
                    result = e;
                }
            }

            return result.item;
        }


        class Entry {
            public int weight;
            public int unlockRound;
            public IPlayerItem item;

            public Entry(IPlayerItem item, int weight, int unlockRound) {
                this.item = item;
                this.weight = weight;
                this.unlockRound = unlockRound;
            }
        }
    }
}
