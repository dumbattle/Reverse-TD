using System.Collections.Generic;


namespace Core {
    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }

        List<CreepAttatchment> allModifiers = new List<CreepAttatchment>();
        List<CreepAttatchment> level1Modifier = new List<CreepAttatchment>();

        public CreepSquad(CreepDefinition def) {
            baseDefinition = def;
            actualDefinition = def.Copy();
        }

        public void AddModifier(CreepAttatchment mod) {
            allModifiers.Add(mod);
            level1Modifier.Add(mod);
            RecalculateActual();
        }

        public int NumModifications() {
            return allModifiers.Count;
        }

        public CreepAttatchment GetAttachment(int index) {
            return allModifiers[index];
        }

        void RecalculateActual() {
            actualDefinition.hp = baseDefinition.hp;
            actualDefinition.speed = baseDefinition.speed;
            actualDefinition.radius = baseDefinition.radius;
            actualDefinition.moneyReward = baseDefinition.moneyReward;
            actualDefinition.count = baseDefinition.count;
            actualDefinition.spacing = baseDefinition.spacing;

            foreach (var l1 in level1Modifier) {
                l1.ApplyModification(baseDefinition, actualDefinition);
            }
        }
    }

}
