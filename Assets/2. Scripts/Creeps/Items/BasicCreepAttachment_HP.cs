using UnityEngine;


namespace Core {
    public class BasicCreepAttachment_HP : CreepAttatchment {
        const float SCALE = 0.3f;

        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            var baseHP = baseDef.hp;
            var additional = baseHP * SCALE;
            result.hp += (int)additional;
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
    }

}
