namespace Core {
    public class GlobalCreeepUpgrades : ICreepDefinitionModifier {
        public float hpScale = 1;

        public void ApplyModification(CreepDefinition baseDef, CreepDefinition result) {
            result.hp += baseDef.hp * (hpScale - 1);
        }
    }
}
