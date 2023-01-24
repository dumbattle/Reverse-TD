using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Count2Speed : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddCountScale(-.3f);
            results.AddSpawnRateScale(-.3f);
            results.AddMoneyScale(.3f);

            results.AddSpdScale(.3f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftCount2Spd;
        }

        public override string GetName() {
            return "Sneak Module";
        }

        public override string GetDescription() {
            return "Less creeps, but faster";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
