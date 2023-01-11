using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_Count : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            var baseState = baseDef.count;
            var additional = baseState * SCALE;
            result.count += additional;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentCount;
        }

        public override string GetName() {
            return "Number Module";
        }

        public override string GetDescription() {
            return "Increases the number of creeps by 30%";
        }
    }

}
