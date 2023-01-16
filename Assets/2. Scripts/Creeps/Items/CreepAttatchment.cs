using UnityEngine;


namespace Core {
    public abstract class CreepAttatchment : ICreepDefinitionModifier, IPlayerItem {
        public abstract string GetDescription();
        public abstract string GetName();
        public abstract Sprite GetIcon();
        public abstract void ApplyModification(CreepDefinition baseDef, CreepDefinition result);
        public abstract CreepModificationLevel GetLevel();


        public static float InverseScale(float baseStat, float currentStat, float scale) {
            float a = 1 / baseStat;
            float b = 1 / currentStat;
            float c = a * scale;
            return 1 / (b + c);
        }

    }
 
    public enum CreepModificationLevel {
        L1,
        L2,
        L3,
    }
}
