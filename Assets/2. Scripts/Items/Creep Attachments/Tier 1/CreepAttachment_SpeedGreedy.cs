using UnityEngine;


namespace Core {
    public class CreepAttachment_SpeedGreedy : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(0.3f);
            results.AddSpeedMinHpScale(1);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.creepAttachmentSpeed;
        }

        public override string GetName() {
            return "Rush Module";
        }

        public override string GetDescription() {
            return "Increases the speed of creeps, but will slow down when hurt";
        }
        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
