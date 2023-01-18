using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_Grouping : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepStatModification results) {
            results.AddSpawnRateScale(SCALE);
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentGroup;
        }

        public override string GetName() {
            return "Grouping Module";
        }

        public override string GetDescription() {
            return "Increases the grouping of creeps by 30%";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }

}
