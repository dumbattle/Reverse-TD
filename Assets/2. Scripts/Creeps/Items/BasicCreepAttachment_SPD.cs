using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_SPD : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            var baseState = baseDef.speed;
            var additional = baseState * SCALE;
            result.speed += additional;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentSpeed;
        }

        public override string GetName() {
            return "Speed Module";
        }

        public override string GetDescription() {
            return "Increases speed by 30%";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L2;
        }
    }

}
