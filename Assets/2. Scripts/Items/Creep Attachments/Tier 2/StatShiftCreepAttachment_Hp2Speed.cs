using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Hp2Speed : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddHpScale(-.3f);
            results.AddSizeScale(-0.2f);

            results.AddSpdScale(.3f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftHp2Speed;
        }

        public override string GetName() {
            return "Haste Module";
        }

        public override string GetDescription() {
            return "Faster, weaker creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
