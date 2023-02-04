using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core {
    public class ResourceType {
        public static ResourceType green { get; } = new ResourceType();
        public static ResourceType red { get; } = new ResourceType();
        public static ResourceType blue { get; } = new ResourceType();
        public static ResourceType yellow { get; } = new ResourceType();
        public static ResourceType diamond { get; } = new ResourceType();

        ResourceType() { }
    }
    public class ResourceRequirement {
        public float this[ResourceType r] {
            get {
                if (r == ResourceType.green) {
                    return green;
                }
                if (r == ResourceType.red) {
                    return red;
                }
                if (r == ResourceType.blue) {
                    return blue;
                }
                if (r == ResourceType.yellow) {
                    return yellow;
                }
                if (r == ResourceType.diamond) {
                    return diamond;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }
       
        protected float green;
        protected float red;
        protected float blue;
        protected float yellow;
        protected float diamond;


        public ResourceRequirement(float green = 0, float red = 0, float blue = 0, float yellow = 0, float diamond = 0) {
            this.green = green;
            this.red = red;
            this.blue = blue;
            this.yellow = yellow;
            this.diamond = diamond;
        }
    }

    public class ResourceCollection : ResourceRequirement {
        public new float this[ResourceType r] {
            get {
                return base[r];
            }
            set {
                if (r == ResourceType.green) {
                    green = value;
                }
                if (r == ResourceType.red) {
                    red = value;
                }
                if (r == ResourceType.blue) {
                    blue = value;
                }
                if (r == ResourceType.yellow) {
                    yellow = value;
                }
                if (r == ResourceType.diamond) {
                    diamond = value;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }
    }

    public class CreepAttachment {
        public CreepAttachmentDefinition definition;
        /// <summary>
        /// Between [1, 10] inclusive
        /// </summary>
        public int level { get; private set; } = 1;

        //***************************************************************************************
        // Control
        //***************************************************************************************

        public void ResetAttachment(CreepAttachmentDefinition def) {
            definition = def;
            level = 1;
        }

        public void UpgradeLevel() {

            level++;
            if (level > 10) {
                level = 10;
            }
        }

        //***************************************************************************************
        // Query
        //***************************************************************************************

        public Sprite GetIcon() {
            return definition?.GetIcon(level) ?? null;
        }

        public string GetName() {
            return definition?.GetName(level) ?? null;
        }

        public string GetDescription() {
            return definition?.GetDescription(level) ?? null;
        }

        public ResourceRequirement GetCostForUpgrade() {
            if (level == 10) {
                return null;
            }

            return definition?.GetCost(level + 1) ?? null;
        }
    }

    public class CreepLoadout {
        public CreepLoadoutSlot specialization { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot resource { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot tier1_1 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_2 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_3 { get; private set; } = GetTier1Slot();

        public CreepLoadoutSlot tier2_1 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier2_2 { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot tier3_A { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier3_B { get; private set; } = new CreepLoadoutSlot();

        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            specialization.GetApplication(stage1, stage2);
            resource.GetApplication(stage1, stage2);

            tier1_1.GetApplication(stage1, stage2);
            tier1_2.GetApplication(stage1, stage2);
            tier1_3.GetApplication(stage1, stage2);

            tier2_1.GetApplication(stage1, stage2);
            tier2_2.GetApplication(stage1, stage2);

            tier3_A.GetApplication(stage1, stage2);
            tier3_B.GetApplication(stage1, stage2);
        }

        static CreepLoadoutSlot GetTier1Slot() {
            return new CreepLoadoutSlot(
                CreepAttachment_Tier1_HP.Get(),
                CreepAttachment_Tier1_SPD.Get(),
                CreepAttachment_Tier1_SpawnRate.Get(),
                CreepAttachment_Tier1_Count.Get()
            );
        }
    }

    public class CreepLoadoutSlot {
        public CreepAttachment currentAttactment { get; } = new CreepAttachment();
        public CreepAttachmentDefinition[] allowedAttachments { get; private set; }

        public CreepLoadoutSlot(params CreepAttachmentDefinition[] allowed) {
            allowedAttachments = allowed;
        }
        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            if (currentAttactment.definition == null) {
                return;
            }

            currentAttactment.definition.ApplyModification(currentAttactment.level, stage1, stage2);
        }
    }

    public abstract class CreepAttachmentDefinition {
        ResourceRequirement[] _upgradeCosts;

        public CreepAttachmentDefinition() {
            _upgradeCosts = InitUpgradeCosts();
        }

        /// <summary>
        /// To unlock provided level. level is 1-indexed
        /// </summary>
        public ResourceRequirement GetCost(int level) {
            level = Mathf.Clamp(level - 1, 0, _upgradeCosts.Length - 1);

            return _upgradeCosts[level];
        }

        /// <summary>
        /// Level = 0 for menu description
        /// </summary>
        public abstract string GetDescription(int level);

        /// <summary>
        /// Level = 0 for menu name 
        /// </summary>
        public abstract string GetName(int level);

        /// <summary>
        /// Level is 1-indexed
        /// </summary>
        public abstract Sprite GetIcon(int level);

        public abstract void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2);


        protected abstract ResourceRequirement[] InitUpgradeCosts();
    }

    public class CreepAttachment_Tier1_SPD : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_SPD instance = new CreepAttachment_Tier1_SPD();
        public static CreepAttachment_Tier1_SPD Get() => instance;

        CreepAttachment_Tier1_SPD() { }
        
        
        static float[] _statScales = { 
            30,
            34,
            39,
            45,
            51,
            59,
            67,
            77,
            88,
            100,
        };

        static string[] _descCache = {
            $"Increases Speed by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases Speed by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases Speed by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "Speed Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemYellow1;
                case 2:
                    return CreepItemIconResourceCache.GemYellow2;
                case 3:
                    return CreepItemIconResourceCache.GemYellow3;
                case 4:
                    return CreepItemIconResourceCache.GemYellow4;
                case 5:
                    return CreepItemIconResourceCache.GemYellow5;
                case 6:
                    return CreepItemIconResourceCache.GemYellow6;
                case 7:
                    return CreepItemIconResourceCache.GemYellow7;
                case 8:
                    return CreepItemIconResourceCache.GemYellow8;
                case 9:
                    return CreepItemIconResourceCache.GemYellow9;
                case 10:
                    return CreepItemIconResourceCache.GemYellow10;
            }

            return CreepItemIconResourceCache.GemYellow1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddSpdScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, yellow: 50),

                new ResourceRequirement(green: 150, yellow: 100),
                new ResourceRequirement(green: 175, yellow: 150),

                new ResourceRequirement(green: 200, yellow: 200),
                new ResourceRequirement(green: 210, yellow: 250, diamond: 100),

                new ResourceRequirement(green: 220, yellow: 300, diamond: 150),
                new ResourceRequirement(green: 230, yellow: 350, diamond: 200),

                new ResourceRequirement(green: 240, yellow: 400, diamond: 250),
                new ResourceRequirement(green: 250, yellow: 450, diamond: 300),
            };
        }
    }

    public class CreepAttachment_Tier1_SpawnRate : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_SpawnRate instance = new CreepAttachment_Tier1_SpawnRate();
        public static CreepAttachment_Tier1_SpawnRate Get() => instance;

        CreepAttachment_Tier1_SpawnRate() { }


        static float[] _statScales = {
            30,
            34,
            39,
            45,
            51,
            59,
            67,
            77,
            88,
            100,
        };

        static string[] _descCache = {
            $"Increases Spawn Rate by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases Spawn Rate by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases Spawn Rate by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "Spawn Rate Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemBlue1;
                case 2:
                    return CreepItemIconResourceCache.GemBlue2;
                case 3:
                    return CreepItemIconResourceCache.GemBlue3;
                case 4:
                    return CreepItemIconResourceCache.GemBlue4;
                case 5:
                    return CreepItemIconResourceCache.GemBlue5;
                case 6:
                    return CreepItemIconResourceCache.GemBlue6;
                case 7:
                    return CreepItemIconResourceCache.GemBlue7;
                case 8:
                    return CreepItemIconResourceCache.GemBlue8;
                case 9:
                    return CreepItemIconResourceCache.GemBlue9;
                case 10:
                    return CreepItemIconResourceCache.GemBlue10;
            }

            return CreepItemIconResourceCache.GemBlue1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddSpawnRateScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, blue: 50),

                new ResourceRequirement(green: 150, blue: 100),
                new ResourceRequirement(green: 175, blue: 150),

                new ResourceRequirement(green: 200, blue: 200),
                new ResourceRequirement(green: 210, blue: 250, diamond: 100),

                new ResourceRequirement(green: 220, blue: 300, diamond: 150),
                new ResourceRequirement(green: 230, blue: 350, diamond: 200),

                new ResourceRequirement(green: 240, blue: 400, diamond: 250),
                new ResourceRequirement(green: 250, blue: 450, diamond: 300),
            };
        }
    }


    public class CreepAttachment_Tier1_Count : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_Count instance = new CreepAttachment_Tier1_Count();
        public static CreepAttachment_Tier1_Count Get() => instance;

        CreepAttachment_Tier1_Count() { }


        static float[] _statScales = {
            30,
            34,
            39,
            45,
            51,
            59,
            67,
            77,
            88,
            100,
        };

        static string[] _descCache = {
            $"Increases the number of creeps by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases the number of creeps by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases the number of creeps by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "Number Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemCyan1;
                case 2:
                    return CreepItemIconResourceCache.GemCyan2;
                case 3:
                    return CreepItemIconResourceCache.GemCyan3;
                case 4:
                    return CreepItemIconResourceCache.GemCyan4;
                case 5:
                    return CreepItemIconResourceCache.GemCyan5;
                case 6:
                    return CreepItemIconResourceCache.GemCyan6;
                case 7:
                    return CreepItemIconResourceCache.GemCyan7;
                case 8:
                    return CreepItemIconResourceCache.GemCyan8;
                case 9:
                    return CreepItemIconResourceCache.GemCyan9;
                case 10:
                    return CreepItemIconResourceCache.GemCyan10;
            }

            return CreepItemIconResourceCache.GemCyan1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddCountScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 90, diamond: 25),
                new ResourceRequirement(green: 105, diamond: 75),

                new ResourceRequirement(green: 120, diamond: 125),
                new ResourceRequirement(green: 135, diamond: 175),

                new ResourceRequirement(green: 150, diamond: 225),
                new ResourceRequirement(green: 165, diamond: 275, blue: 100),

                new ResourceRequirement(green: 180, diamond: 325, blue: 150),
                new ResourceRequirement(green: 195, diamond: 375, blue: 200),

                new ResourceRequirement(green: 210, diamond: 425, blue: 250),
                new ResourceRequirement(green: 225, diamond: 475, blue: 300),
            };
        }
    }




    public class CreepAttachment_Tier1_HP : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_HP instance = new CreepAttachment_Tier1_HP();
        public static CreepAttachment_Tier1_HP Get() => instance;

        CreepAttachment_Tier1_HP() { }


        static float[] _statScales = {
            30,
            34,
            39,
            45,
            51,
            59,
            67,
            77,
            88,
            100,
        };

        static string[] _descCache = {
            $"Increases HP by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases HP by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases HP by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "HP Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemRed1;
                case 2:
                    return CreepItemIconResourceCache.GemRed2;
                case 3:
                    return CreepItemIconResourceCache.GemRed3;
                case 4:
                    return CreepItemIconResourceCache.GemRed4;
                case 5:
                    return CreepItemIconResourceCache.GemRed5;
                case 6:
                    return CreepItemIconResourceCache.GemRed6;
                case 7:
                    return CreepItemIconResourceCache.GemRed7;
                case 8:
                    return CreepItemIconResourceCache.GemRed8;
                case 9:
                    return CreepItemIconResourceCache.GemRed9;
                case 10:
                    return CreepItemIconResourceCache.GemRed10;
            }

            return CreepItemIconResourceCache.GemRed1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddHpScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, red: 50),

                new ResourceRequirement(green: 150, red: 100),
                new ResourceRequirement(green: 175, red: 150),

                new ResourceRequirement(green: 200, red: 200),
                new ResourceRequirement(green: 210, red: 250, diamond: 100),

                new ResourceRequirement(green: 220, red: 300, diamond: 150),
                new ResourceRequirement(green: 230, red: 350, diamond: 200),

                new ResourceRequirement(green: 240, red: 400, diamond: 250),
                new ResourceRequirement(green: 250, red: 450, diamond: 300),
            };
        }
    }







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
        public CreepLoadout loadout = new CreepLoadout();
        //List<IPlayerItem> allAttachments = new List<IPlayerItem>();
        //List<ICreepDefinitionModifier> level1Modifier = new List<ICreepDefinitionModifier>();
        //List<ICreepDefinitionModifier> level2Modifier = new List<ICreepDefinitionModifier>();
        //List<ICreepDefinitionModifier> level3Modifier = new List<ICreepDefinitionModifier>();

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
            //allAttachments.Add(item);
            //if (item is CreepAttatchment atch) {
            //    GetLevelList(atch.GetLevel()).Add(atch);
            //    Recalculate();
            //}
        }
        
        public void RemoveItem(int index) {
            //var item = allAttachments[index];
            //allAttachments.RemoveAt(index);
            //if (item is CreepAttatchment atch) {
            //    GetLevelList(atch.GetLevel()).Remove(atch);
            //    Recalculate();
            //}
        }
        
        public int NumAttachments() {
            return 0;
            //return allAttachments.Count;
        }

        public IPlayerItem GetAttachment(int index) {
            return null;
            //if (index >= allAttachments.Count) {
            //    return null;
            //}
            //return allAttachments[index];
        }
       
        public void Recalculate() {
            stage1.Reset();
            stage2.Reset();
            actualDefinition.CopyFrom(baseDefinition);

            globalUpgrades.ApplyModification(stage1);
            loadout.GetApplication(stage1, stage2);


            levelModifiers?.Apply(actualDefinition);
            stage1.Apply(actualDefinition);
            stage2.Apply(actualDefinition);

            //foreach (var l in level1Modifier) {
            //    l.ApplyModification(stage1);
            //}

            //foreach (var l in level2Modifier) {
            //    l.ApplyModification(stage2);
            //}

            //levelModifiers?.Apply(actualDefinition);
            //stage1.Apply(actualDefinition);
            //stage2.Apply(actualDefinition);

            //// death split child
            //actualDefinition.deathSplitDefinition = null;
            //int deathSplitCount = stage1.deathSpawnLevel + stage2.deathSpawnLevel;
            //deathSplitActive = false;
            //if (!isChild && deathSplitCount > 0) {
            //    if (deathSplitSquad == null) {
            //        deathSplitSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
            //        deathSplitSquad.parentSquad = this;
            //        deathSplitSquad.baseDefinition.speed *= .8f;
            //        deathSplitSquad.baseDefinition.radius /= 2f;
            //        deathSplitSquad.isDeathSpawn = true;
            //        deathSplitSquad.baseDefinition.glowColor = UnityEngine.Color.black;
            //    }

            //    deathSplitSquad.baseDefinition.hp = baseDefinition.hp / (1f + deathSplitCount);
            //    deathSplitSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / (1f + deathSplitCount);
            //    deathSplitSquad.baseDefinition.count = deathSplitCount;
            //    deathSplitSquad.baseDefinition.spawnRate = 0;

            //    deathSplitSquad.Recalculate();
            //    actualDefinition.deathSplitDefinition = deathSplitSquad.actualDefinition;
            //    deathSplitActive = true;
            //}

            //// carrier child
            //actualDefinition.carrierDefinition = null;
            //int carrierLevel = stage1.carrierSpawnLevel + stage2.carrierSpawnLevel;
            //carrierActive = false;
            //if (!isChild && carrierLevel > 0) {
            //    if (carrierSquad == null) {
            //        carrierSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
            //        carrierSquad.parentSquad = this;
            //        carrierSquad.baseDefinition.radius /= 2f;
            //        carrierSquad.isCarrierSpawn = true;
            //        carrierSquad.baseDefinition.glowColor = UnityEngine.Color.white;
            //    }
            //    carrierSquad.baseDefinition.speed = baseDefinition.speed * 1.5f;

            //    carrierSquad.baseDefinition.hp = baseDefinition.hp / 40f * (10 + carrierLevel);
            //    carrierSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / 2;
            //    carrierSquad.baseDefinition.count = 1;
            //    carrierSquad.baseDefinition.spawnRate = 0.08f + 0.02f * carrierLevel;

            //    carrierSquad.Recalculate();
            //    actualDefinition.carrierDefinition = carrierSquad.actualDefinition;
            //    carrierActive = true;
            //}
        }

        public CreepSquad GetDeathSplitSquad() {
            return deathSplitActive ? deathSplitSquad : null;
        }

        public CreepSquad GetCarrierSquad() {
            return carrierActive ? carrierSquad : null;
        }
    }
}
