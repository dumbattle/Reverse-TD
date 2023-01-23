using UnityEngine;


namespace Core {
    public class StatShiftCreepAttachment_Speed2HP : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(-.3f);
            results.AddHpScale(.3f);
            results.AddSizeScale(0.2f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftSpeed2Hp;
        }

        public override string GetName() {
            return "Tank Module";
        }

        public override string GetDescription() {
            return "Gives a creep more HP, but lowers speed";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Count2HP : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddCountScale(-.3f);
            results.AddSpawnRateScale(-.3f);

            results.AddHpScale(.3f);
            results.AddSizeScale(0.2f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftCount2Hp;
        }

        public override string GetName() {
            return "Merge Module";
        }

        public override string GetDescription() {
            return "Gives a creep more HP, but fewer creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Count2Speed : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddCountScale(-.3f);
            results.AddSpawnRateScale(-.3f);

            results.AddSpdScale(.3f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftCount2Spd;
        }

        public override string GetName() {
            return "Sneak Module";
        }

        public override string GetDescription() {
            return "Less creeps, but faster";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Hp2Speed : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddHpScale(-.3f);
            results.AddSizeScale(-0.2f);

            results.AddSpdScale(.3f);
        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftHp2Speed;
        }

        public override string GetName() {
            return "Haste Module";
        }

        public override string GetDescription() {
            return "Faster, weaker creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Hp2Count : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddHpScale(-.3f);
            results.AddSizeScale(-0.2f);

            results.AddCountScale(.3f);
            results.AddSpawnRateScale(.3f);

        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftHp2Count;
        }

        public override string GetName() {
            return "Swarm Module";
        }

        public override string GetDescription() {
            return "Lots of weaker creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
    public class StatShiftCreepAttachment_Speed2Count : CreepAttatchment {
        public override void ApplyModification(CreepStatModification results) {
            results.AddSpdScale(-.3f);

            results.AddCountScale(.3f);
            results.AddSpawnRateScale(.3f);

        }

        public override Sprite GetIcon() {
            return CreepItemIconResourceCache.shiftSpeed2Count;
        }

        public override string GetName() {
            return "Horde Module";
        }

        public override string GetDescription() {
            return "Lots of slower creeps";
        }

        public override CreepModificationLevel GetLevel() {
            return CreepModificationLevel.L1;
        }
    }
}
