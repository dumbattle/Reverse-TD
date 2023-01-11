namespace Core {
    public interface ICreepDefinitionModifier {
        void ApplyModification(CreepDefinition baseDef, CreepDefinition result);
    }

    public enum CreepModificationModificationLevel {
        L1_flat,
        L2_scale
    }
}
