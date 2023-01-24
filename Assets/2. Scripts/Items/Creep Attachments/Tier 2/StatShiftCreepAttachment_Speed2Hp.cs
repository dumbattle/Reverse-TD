using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Speed2Hp : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(-.3f);
            results.AddHpScale(.3f);
            results.AddSizeScale(0.2f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftSpeed2Hp;
        }

        public override string GetName() {
            return "Tank Module";
        }

        public override string GetDescription() {
            return "Gives a creep more HP, but lowers speed";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
