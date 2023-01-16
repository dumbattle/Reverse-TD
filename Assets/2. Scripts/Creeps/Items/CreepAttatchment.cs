using UnityEngine;


namespace Core {
    public abstract class CreepAttatchment : ICreepDefinitionModifier, IPlayerItem {
        public abstract string GetDescription();
        public abstract string GetName();
        public abstract Sprite GetIcon();
        public abstract void ApplyModification(CreepStatModification results);
        public abstract CreepModificationLevel GetLevel();


        public static float InverseScale(float baseStat, float currentStat, float scale) {
            return 1 / (1 / currentStat + 1 / baseStat * scale);
        }

    }
 
    public enum CreepModificationLevel {
        L1,
        L2,
        L3,
    }
}
