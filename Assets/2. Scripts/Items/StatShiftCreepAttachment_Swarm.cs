using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Swarm : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSizeScale(-0.35f);

            results.AddHpScale(-1);
            results.AddCountScale(1);
            results.AddSpawnRateScale(1.3f);
            results.AddMoneyScale(-1);
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachment_TankShift;
        }

        public override string GetName() {
            return "Swarm Module";
        }

        public override string GetDescription() {
            return "Multiplies the creep";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
