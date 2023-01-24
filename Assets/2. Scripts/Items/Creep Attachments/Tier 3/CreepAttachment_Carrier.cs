using UnityEngine;


namespace Core {
    public class CreepAttachment_Carrier: CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.carrierSpawnLevel++;

        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftCount2Hp;
        }

        public override string GetName() {
            return "Carrier Module";
        }

        public override string GetDescription() {
            return "Periodically spawns creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }

        public override bool Attachable(CreepSquad s) {
            return !s.isChild;
        }
    }
}
