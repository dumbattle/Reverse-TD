using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Speed : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(.7f);
            results.AddHpScale(-.6f);
            results.AddCountScale(-.6f);
            results.AddMoneyScale(.6f);
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachment_SpeedShift;
        }

        public override string GetName() {
            return "Swift Module";
        }

        public override string GetDescription() {
            return "Makes a creep more speedy";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
