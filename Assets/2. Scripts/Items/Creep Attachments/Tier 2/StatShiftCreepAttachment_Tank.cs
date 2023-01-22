using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Tank : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSizeScale(0.2f);

            results.AddHpScale(1);
            results.AddSpdScale(-.7f);
            results.AddCountScale(-1f);
            results.AddSpawnRateScale(-1.3f); // lower hp/sec
            results.AddMoneyScale(1);
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachment_TankShift;
        }

        public override string GetName() {
            return "Tank Module";
        }

        public override string GetDescription() {
            return "Makes a creep more tanky";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
