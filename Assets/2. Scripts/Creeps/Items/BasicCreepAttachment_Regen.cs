using UnityEngine;


namespace Core {

    public class BasicCreepAttachment_Regen : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.regenRate += 0.01f;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentRegen;
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
