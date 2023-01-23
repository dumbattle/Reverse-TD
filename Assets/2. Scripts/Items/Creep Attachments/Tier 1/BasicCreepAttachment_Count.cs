using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_Count : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepStatModification results) {
            results.AddCountScale(SCALE);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.basicCount;
        }

        public override string GetName() {
            return "Number Module";
        }

        public override string GetDescription() {
            return "Increases the number of creeps by 30%";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }

}
