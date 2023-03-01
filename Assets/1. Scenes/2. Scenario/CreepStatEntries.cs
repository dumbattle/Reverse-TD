using Core;
using UnityEngine;


namespace GameUI.CreepMenus {
    [System.Serializable]
    public class CreepStatEntries {
        public GameObject root;
        public CreepStatEntryBehaviour hp;
        public CreepStatEntryBehaviour count;
        public CreepStatEntryBehaviour dmgMult;
        public CreepStatEntryBehaviour spd;
        public CreepStatEntryBehaviour spawnRate;
        public CreepStatEntryBehaviour incomeMult;


        public void Redraw(ScenarioInstance s, CreepSquad c) {
            hp.Redraw(s, c.stats.hp);
            count.Redraw(s, c.stats.count);
            dmgMult.Redraw(s, c.stats.damageMult);
            spd.Redraw(s, c.stats.spd);
            spawnRate.Redraw(s, c.stats.spawnRate);
            incomeMult.Redraw(s, c.stats.incomeMult);
            root.gameObject.SetActive(true);
        }

        public void Close() {
            root.gameObject.SetActive(false);
        }
    }
}
