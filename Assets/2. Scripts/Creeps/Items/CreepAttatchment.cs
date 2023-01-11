using UnityEngine;


namespace Core {
    public abstract class CreepAttatchment : ICreepDefinitionModifier, IPlayerItem {
        public abstract string GetDescription();
        public abstract string GetName();
        public abstract Sprite GetIcon();
        public abstract void ApplyModification(CreepDefinition baseDef, CreepDefinition result);
    }

}
