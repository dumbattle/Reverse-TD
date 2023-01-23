using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_Regen : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.regenRate += 0.01f;
            results.AddShrinkMinHp(0.5f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.basicRegen;
        }

        public override string GetName() {
            return "Regeneration Module";
        }

        public override string GetDescription() {
            return "Increases the HP renegeration of creeps by 1% hp/s";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }
}
