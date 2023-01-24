using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Speed2Count : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(-.3f);

            results.AddCountScale(.3f);
            results.AddSpawnRateScale(.3f);
            results.AddMoneyScale(-.3f);

        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftSpeed2Count;
        }

        public override string GetName() {
            return "Horde Module";
        }

        public override string GetDescription() {
            return "Lots of slower creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
