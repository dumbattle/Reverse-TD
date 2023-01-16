using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Tank : CreepAttatchment {
        const float SCALE = 0.6f;


        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            result.radius += baseDef.radius * .15f;
            result.radius = Mathf.Min(result.radius, 0.45f);
            result.hp += baseDef.hp * SCALE;
            result.speed = InverseScale(baseDef.speed, result.speed, SCALE);
            result.count = InverseScale(baseDef.count, result.count, SCALE);
            result.spacing += baseDef.spacing * SCALE;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachment_TankShift;
        }

        public override string GetName() {
            return "Tank Module";
        }

        public override string GetDescription() {
            return "Makes a creep more tanky";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Speed : CreepAttatchment {
        const float SCALE = 0.6f;


        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {

            result.speed += baseDef.speed * SCALE;
            result.hp = InverseScale(baseDef.hp, result.hp, SCALE);
            result.count = InverseScale(baseDef.count, result.count, SCALE);
            result.spacing += baseDef.spacing * SCALE;
        }

        public override Sprite GetIcon() {
            return IconResourceCache.creepAttachment_SpeedShift;
        }

        public override string GetName() {
            return "Speed Module";
        }

        public override string GetDescription() {
            return "Makes a creep more speedy";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }

    public class BasicCreepAttachment_Regen : CreepAttatchment {
        public override void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            result.hpRegenRate += 0.01f;
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
