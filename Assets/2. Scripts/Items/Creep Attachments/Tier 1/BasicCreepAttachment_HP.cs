using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_HP : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepStatModification results) {
            results.AddHpScale(SCALE);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.creepAttachmentHP;
        }

        public override string GetName() {
            return "HP Module";
        }

        public override string GetDescription() {
            return "Increases HP by 30%";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }

}
