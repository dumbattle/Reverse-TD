using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_SpawnRate : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepStatModification results) {
            results.AddSpawnRateScale(SCALE);
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentGroup;
        }

        public override string GetName() {
            return "Efficiency Module";
        }

        public override string GetDescription() {
            return "Increases the spawn rate of creeps by 30%";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }

}
