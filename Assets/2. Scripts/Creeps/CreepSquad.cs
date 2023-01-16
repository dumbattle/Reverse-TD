using System.Collections.Generic;


namespace Core {
    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }
        CreepDefinition temp;

        List<CreepAttatchment> allModifiers = new List<CreepAttatchment>();
        List<CreepAttatchment> level1Modifier = new List<CreepAttatchment>();
        List<CreepAttatchment> level2Modifier = new List<CreepAttatchment>();
        List<CreepAttatchment> level3Modifier = new List<CreepAttatchment>();

        GlobalCreeepUpgrades globalUpgrades;


        public CreepSquad(CreepDefinition def, GlobalCreeepUpgrades globalUpgrades) {
            baseDefinition = def;
            actualDefinition = def.CreateCopy();
            temp = new CreepDefinition();
            this.globalUpgrades = globalUpgrades;
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
            return allModifiers[index];
        }

        public void RecalculateActual() {
            temp.CopyFrom(baseDefinition);

            globalUpgrades.ApplyModification(baseDefinition, temp);


            actualDefinition.CopyFrom(temp);
            foreach (var l in level1Modifier) {
                l.ApplyModification(temp, actualDefinition);
            }


            temp.CopyFrom(actualDefinition);
            foreach (var l in level2Modifier) {
                l.ApplyModification(actualDefinition, temp);
            }


            actualDefinition.CopyFrom(temp);
            foreach (var l in level3Modifier) {
                l.ApplyModification(temp, actualDefinition);
            }

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
