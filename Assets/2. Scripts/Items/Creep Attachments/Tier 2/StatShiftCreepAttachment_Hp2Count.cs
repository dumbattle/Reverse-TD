using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Hp2Count : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddHpScale(-.3f);
            results.AddSizeScale(-0.2f);

            results.AddCountScale(.3f);
            results.AddSpawnRateScale(.3f);
            results.AddMoneyScale(-.3f);

        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftHp2Count;
        }

        public override string GetName() {
            return "Swarm Module";
        }

        public override string GetDescription() {
            return "Lots of weaker creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
