using System.Collections.Generic;


namespace Core {
    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }

        public CreepSquad parentSquad { get; private set; }
        CreepSquad deathSplitSquad;
        CreepSquad carrierSquad;

        public bool isChild => isDeathSpawn || isCarrierSpawn;
        public bool isDeathSpawn { get; private set; }
        public bool isCarrierSpawn { get; private set; }


        bool deathSplitActive;
        bool carrierActive;
        //**********************************************************************************************
        // Collections
        //**********************************************************************************************

        List<IPlayerItem> allAttachments = new List<IPlayerItem>();
        List<ICreepDefinitionModifier> level1Modifier = new List<ICreepDefinitionModifier>();
        List<ICreepDefinitionModifier> level2Modifier = new List<ICreepDefinitionModifier>();
        List<ICreepDefinitionModifier> level3Modifier = new List<ICreepDefinitionModifier>();

        //**********************************************************************************************
        // Upgrades
        //**********************************************************************************************

        GlobalCreeepUpgrades globalUpgrades;
        CreepStatModification levelModifiers;

        CreepStatModification stage1 = new CreepStatModification();
        CreepStatModification stage2 = new CreepStatModification();


        public CreepSquad(CreepDefinition def, GlobalCreeepUpgrades globalUpgrades, CreepStatModification levelModifiers) {
            this.levelModifiers = levelModifiers;
            baseDefinition = def;
            actualDefinition = def.CreateCopy();
            this.globalUpgrades = globalUpgrades;

            Recalculate();
        }

        public void AddModifier(IPlayerItem item) {
            allAttachments.Add(item);
            if (item is CreepAttatchment atch) {
                GetLevelList(atch.GetLevel()).Add(atch);
                Recalculate();
            }
        }
        
        public void RemoveItem(int index) {
            var item = allAttachments[index];
            allAttachments.RemoveAt(index);
            if (item is CreepAttatchment atch) {
                GetLevelList(atch.GetLevel()).Remove(atch);
                Recalculate();
            }
        }
        
        public int NumAttachments() {
            return allAttachments.Count;
        }

        public IPlayerItem GetAttachment(int index) {
            if (index >= allAttachments.Count) {
                return null;
            }
            return allAttachments[index];
        }
       
        public void Recalculate() {
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

            levelModifiers?.Apply(actualDefinition);
            stage1.Apply(actualDefinition);
            stage2.Apply(actualDefinition);

            // death split child
            actualDefinition.deathSplitDefinition = null;
            int deathSplitCount = stage1.deathSpawnLevel + stage2.deathSpawnLevel;
            deathSplitActive = false;
            if (!isChild && deathSplitCount > 0) {
                if (deathSplitSquad == null) {
                    deathSplitSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
                    deathSplitSquad.parentSquad = this;
                    deathSplitSquad.baseDefinition.speed *= .8f;
                    deathSplitSquad.baseDefinition.radius /= 2f;
                    deathSplitSquad.isDeathSpawn = true;
                    deathSplitSquad.baseDefinition.glowColor = UnityEngine.Color.black;
                }

                deathSplitSquad.baseDefinition.hp = baseDefinition.hp / (1f + deathSplitCount);
                deathSplitSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / (1f + deathSplitCount);
                deathSplitSquad.baseDefinition.count = deathSplitCount;
                deathSplitSquad.baseDefinition.spawnRate = 0;

                deathSplitSquad.Recalculate();
                actualDefinition.deathSplitDefinition = deathSplitSquad.actualDefinition;
                deathSplitActive = true;
            }

            // carrier child
            actualDefinition.carrierDefinition = null;
            int carrierLevel = stage1.carrierSpawnLevel + stage2.carrierSpawnLevel;
            carrierActive = false;
            if (!isChild && carrierLevel > 0) {
                if (carrierSquad == null) {
                    carrierSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
                    carrierSquad.parentSquad = this;
                    carrierSquad.baseDefinition.radius /= 2f;
                    carrierSquad.isCarrierSpawn = true;
                    carrierSquad.baseDefinition.glowColor = UnityEngine.Color.white;
                }
                carrierSquad.baseDefinition.speed = baseDefinition.speed * 1.5f;

                carrierSquad.baseDefinition.hp = baseDefinition.hp / 40f * (10 + carrierLevel);
                carrierSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / 2;
                carrierSquad.baseDefinition.count = 1;
                carrierSquad.baseDefinition.spawnRate = 0.08f + 0.02f * carrierLevel;

                carrierSquad.Recalculate();
                actualDefinition.carrierDefinition = carrierSquad.actualDefinition;
                carrierActive = true;
            }
        }

        public CreepSquad GetDeathSplitSquad() {
            return deathSplitActive ? deathSplitSquad : null;
        }
        public CreepSquad GetCarrierSquad() {
            return carrierActive ? carrierSquad : null;
        }
        List<ICreepDefinitionModifier> GetLevelList(CreepModificationLevel level) {
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
