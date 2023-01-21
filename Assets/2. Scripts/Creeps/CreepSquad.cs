using System.Collections.Generic;


namespace Core {
    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }

        List<CreepAttatchment> allModifiers = new List<CreepAttatchment>();
        List<CreepAttatchment> level1Modifier = new List<CreepAttatchment>();
        List<CreepAttatchment> level2Modifier = new List<CreepAttatchment>();
        List<CreepAttatchment> level3Modifier = new List<CreepAttatchment>();

        GlobalCreeepUpgrades globalUpgrades;
        CreepStatModification levelModifiers;
        CreepStatModification stage1 = new CreepStatModification();
        CreepStatModification stage2 = new CreepStatModification();

        public CreepSquad(CreepDefinition def, GlobalCreeepUpgrades globalUpgrades, CreepStatModification levelModifiers) {
            this.levelModifiers = levelModifiers;
            baseDefinition = def;
            actualDefinition = def.CreateCopy();
            this.globalUpgrades = globalUpgrades;

            RecalculateActual();
        }

        public void AddModifier(CreepAttatchment mod) {
            allModifiers.Add(mod);
            GetLevelList(mod.GetLevel()).Add(mod);
            RecalculateActual();
        }

        public int NumModifications() {
            return allModifiers.Count;
        }

        public CreepAttatchment GetAttachment(int index) {
            if (index >= allModifiers.Count) {
                return null;
            }
            return allModifiers[index];
        }

        public void RecalculateActual() {
            stage1.Reset();
            stage2.Reset();
            actualDefinition.CopyFrom(baseDefinition);

            globalUpgrades.ApplyModification(stage1);


            foreach (var l in level1Modifier) {
                l.ApplyModification(stage1);
            }


            foreach (var l in level2Modifier) {
                l.ApplyModification(stage2);
            }

            levelModifiers.Apply(actualDefinition);
            stage1.Apply(actualDefinition);
            stage2.Apply(actualDefinition);
        }


        List<CreepAttatchment> GetLevelList(CreepModificationLevel level) {
            switch (level) {
                case CreepModificationLevel.L1:
                    return level1Modifier;
                case CreepModificationLevel.L2:
                    return level2Modifier;
                case CreepModificationLevel.L3:
                    return level3Modifier;
            }
            return null;
        }
    }

}
