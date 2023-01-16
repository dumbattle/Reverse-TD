namespace Core {
    public class GlobalCreeepUpgrades : ICreepDefinitionModifier {
        public float hpScale = 1;

        public void ApplyModification(CreepStatModification results) {
            results.AddHpScale(hpScale - 1);
        }
    }
}
