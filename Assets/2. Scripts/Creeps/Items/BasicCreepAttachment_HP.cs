using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_HP : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            var baseHP = baseDef.hp;
            var additional = baseHP * SCALE;
            result.hp += additional;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachmentHP;
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
