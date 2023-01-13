﻿using System.Collections.Generic;


namespace Core {
    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }

        List<CreepAttatchment> allModifiers = new List<CreepAttatchment>();
        List<CreepAttatchment> level1Modifier = new List<CreepAttatchment>();
        GlobalCreeepUpgrades globalUpgrades;


        public CreepSquad(CreepDefinition def, GlobalCreeepUpgrades globalUpgrades) {
            baseDefinition = def;
            actualDefinition = def.Copy();
            this.globalUpgrades = globalUpgrades;
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

        public void RecalculateActual() {
            actualDefinition.hp = baseDefinition.hp;
            actualDefinition.speed = baseDefinition.speed;
            actualDefinition.radius = baseDefinition.radius;
            actualDefinition.moneyReward = baseDefinition.moneyReward;
            actualDefinition.count = baseDefinition.count;
            actualDefinition.spacing = baseDefinition.spacing;

            foreach (var l1 in level1Modifier) {
                l1.ApplyModification(baseDefinition, actualDefinition);
            }
            globalUpgrades.ApplyModification(baseDefinition, actualDefinition);
        }
    }

}
